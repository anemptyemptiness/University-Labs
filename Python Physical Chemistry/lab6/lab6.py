import matplotlib.pyplot as plt
import math

R = 8.315  # Дж/(моль∙К)
T = 543  # K

# Задание 1
t = [0, 1200, 2400, 6000, 12000, 18000, 24000]  # секунды
p = [3890, 4000, 4090, 4330, 4610, 4800, 4940]  # Паскаль
Pa = [3 * p[0] - 2 * p[i] for i in range(7)]  # Паскаль
Ca = [Pa[i] / (R * T) for i in range(7)]  # кмоль/м^3

# Расчёт константы скорости по Excel
k = 6E-05
print('Константа скорости: ' + str(k) + ' c^-1')

# Расчёт концентрации вещества при t = 15000 sec
t1 = 15000  # seconds
C_t1 = Ca[0] / (1 + Ca[0] * k * t1)
print('Концентрация вещества при t = 15000sec: ' + str(C_t1) + ' kmol/m^3')

# Расчёт порядка реакции 2F2O = 2F2 + O2
# 2 порядок (в Excel)

# Расчёт времени полупревращения
r = 1 / (k * Ca[0])
print('Время полупревращения r1/2: ' + str(r) + ' sec')

# Расчёт степени превращения
res = (Ca[0] - C_t1) / Ca[0]
print('Степень превращения: ' + str(res))

# Кинетическая кривая концентрации реагента
# plt.plot(t, Ca)
# plt.title('Кинетическая кривая концентрации реагента')
# plt.xlabel('t, сек')
# plt.ylabel('C, кмоль/м^3')
# plt.show()

# Задание 2
k1 = 0.18
k2 = 0.29

delta_t = 0.01
t = [delta_t * i for i in range(0, 4100)]

Ca = [0.028]  # кмоль/м^3
Cb = [0]  # кмоль/м^3
Cc = [0]  # кмоль/м^3
Cd = [0]  # кмоль/м^3

for i in range(1, 4100):
    Ca.append(-k1 * Ca[i - 1] * delta_t + Ca[i - 1])
    Cb.append(k1 * Ca[i - 1] * delta_t - k2 * Cb[i - 1] * Cc[i - 1] * delta_t + Cb[i - 1])
    Cc.append(k1 * Ca[i - 1] * delta_t - k2 * Cb[i - 1] * Cb[i - 1] * delta_t + Cc[i - 1])
    Cd.append(k1 * Cb[i - 1] * Cc[i - 1] * delta_t + Cd[i - 1])

# Построение графиков
plt.plot(t, Ca, 'red')
plt.plot(t, Cb, 'green')
plt.plot(t, Cc, 'blue')
plt.plot(t, Cd, 'pink')
plt.title('График изменения концентрации i-С4H10 при T=const')
plt.xlabel('t, сек')
plt.ylabel('C, кмоль/м^3')
plt.show()

