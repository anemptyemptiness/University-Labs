import numpy as np
from scipy.optimize import minimize
import matplotlib.pyplot as plt


def experimental_gibbs(exp_x1, exp_y1, pressure): # вычисляем энергию Гиббса по экспериментальным данным
    exp_x2 = np.array([1 - i for i in exp_x1])
    exp_y2 = np.array([1 - i for i in exp_y1])
    # расчет нулевого давления p0
    press_a = np.exp(18.30 - 3816.44 / (-46.13 + 298.15)) / 750.0668
    press_b = np.exp(16.81 - 3405.57 / (-56.34 + 298.15)) / 750.0668
    # функция для расчета гамм
    gamma_f = lambda x, y, pressure, press_0: (y * pressure) / (x * press_0)
    gamma_1_exp = [gamma_f(exp_x1[i], exp_y1[i], pressure[i], press_a) for i in range(len(exp_x1))]
    gamma_2_exp = [gamma_f(exp_x2[i], exp_y2[i], pressure[i], press_b) for i in range(len(exp_x1))]
    # экспериментальная формула для расчета избыточной энергии Гиббса
    g_experimental = np.array(
        [8.314 * t * (exp_x1[i] * np.log(gamma_1_exp[i]) + exp_x2[i] * np.log(gamma_2_exp[i])) for i in
         range(len(exp_x1))])
    return g_experimental

# модель Вильсона
def model_wilson(alpha12, alpha21, exp_x1, t, v1_l, v2_l):
    exp_x2 = np.array([1 - i for i in exp_x1])
    # вычисляем дельты по формуле
    delta12 = (v2_l / v1_l) * np.exp(-alpha12 / (8.314 * t))
    delta21 = (v1_l / v2_l) * np.exp(-alpha21 / (8.314 * t))
    # получение энергии Вилсона через дельты
    g_wilson = 8.314 * t * (
            -exp_x1 * np.log(abs(exp_x1 + delta12 * exp_x2)) - exp_x2 * np.log(abs(exp_x2 + delta21 * exp_x1)))
    return g_wilson

# задаем искомые известные значения
t = 298.15
x1 = np.array([0.1, 0.3, 0.5, 0.7, 0.9])
x2 = np.array([1 - i for i in x1])
y1 = np.array([0.250, 0.480, 0.642, 0.706, 0.867])
y2 = [1 - i for i in y1]
press = [0.0244, 0.0296, 0.0324, 0.0335, 0.0331]
g_exp = experimental_gibbs(x1, y1, press)

Tc = [647.30, 594.40]
Pc = [220.48 * 1e5, 57.86 * 1e5]
omega = [0.34, 0.45]
v1 = (((((8.314 * Tc[0]) / Pc[0]) * np.power(0.29056 - 0.08775 * omega[0], 1 + (1 - (t / Tc[0])) ** (2.0 / 7)))))
v2 = (((((8.314 * Tc[1]) / Pc[1]) * np.power(0.29056 - 0.08775 * omega[1], 1 + (1 - (t / Tc[1])) ** (2 / 7)))))
# функция, которой мы будем минимизировать
minimize_it = lambda alphas: np.sqrt(np.sum((np.array(g_exp) - model_wilson(alphas[0], alphas[1], x1, t, v1, v2)) ** 2))

x_init = np.array([1000, 1000])
answ = minimize(minimize_it, x_init, method="Nelder-mead", tol=1e-6).x
print(answ)

# равномерное распределение значений х в заданном диапазоне
x1_new = np.linspace(0, 1, 1000)
x2_new = np.array([1 - i for i in x1_new])
delta12 = (v2 / v1) * np.exp(-answ[0] / (8.314 * t))
delta21 = (v1 / v2) * np.exp(-answ[1] / (8.314 * t))
gamma1 = np.exp(-np.log(x1_new + delta12 * x2_new) + x2_new * (
            (delta12 / (x1_new + delta12 * x2_new)) - (delta21 / (x2_new + delta21 * x1_new))))
gamma2 = np.exp(-np.log(x2_new + delta21 * x1_new) - x1_new * (
            (delta12 / (x1_new + delta12 * x2_new)) - (delta21 / (x2_new + delta21 * x1_new))))
press_a = np.exp(18.30 - 3816.44 / (-46.13 + 298.15)) / 750.0668
press_b = np.exp(16.81 - 3405.57 / (-56.34 + 298.15)) / 750.0668
x1_new[0] += 1e-12
x2_new[-1] += 1e-12
y1_new = np.nan_to_num((gamma1 * x1_new * press_a) / (gamma2 * x2_new * press_b))
y1_new = y1_new / (y1_new + 1)
y2_new = np.array([1 - i for i in y1_new])
press_new = (gamma1 * x1_new * press_a) / y1_new

# рисуем графики
plt.figure()
plt.plot(x1_new, y1_new)
plt.scatter(x1, y1, 5, "green")
plt.figure()
plt.plot(x2_new, y2_new)
plt.scatter(x2, y2, 5, "green")
plt.figure()
plt.plot(x1_new, press_new)
plt.plot(y1_new, press_new)
plt.scatter(x1, press, 5, "green")
plt.scatter(y1, press, 5, "green")
plt.show()
