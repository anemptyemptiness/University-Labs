from thermo import ChemicalConstantsPackage, CEOSGas, CEOSLiquid, PRMIX, FlashVL, FlashVLN
from thermo.interaction_parameters import IPDB
import matplotlib.pyplot as plt

# загрузим константы и свойства
constants, properties = ChemicalConstantsPackage.from_IDs(['ETHANOL', '2-PROPANOL'])
# Объекты инициализируются при определенном условии
T = 333.15
P = 1e5
zs = [0.5, 0.5]

# Используйте Peng-Robinson как для газовой, так и для жидкой фаз
k12 = -0.06
kijs = [[0, k12],
        [k12, 0]]
print('Kij: ' + str(k12))

eos_kwargs = dict(Tcs=constants.Tcs, Pcs=constants.Pcs, omegas=constants.omegas, kijs=kijs)
gas = CEOSGas(PRMIX, eos_kwargs, HeatCapacityGases=properties.HeatCapacityGases, T=T, P=P, zs=zs)
liquid = CEOSLiquid(PRMIX, eos_kwargs, HeatCapacityGases=properties.HeatCapacityGases, T=T, P=P, zs=zs)

# Создайте экземпляр flasher, предполагающий только парожидкостное поведение
flasher = FlashVL(constants, properties, liquid=liquid, gas=gas)

# Создание a T-xy plot at P bar
_ = flasher.plot_Txy(P=P, pts=100)

# Создание a P-xy plot at T Kelvin
_ = flasher.plot_Pxy(T=T, pts=100)

# Создание a xy diagram at T Kelvin
_ = flasher.plot_xy(T=T, pts=100)

liquid2 = CEOSLiquid(PRMIX, eos_kwargs, HeatCapacityGases=properties.HeatCapacityGases, T=T, P=P, zs=zs)
flasher2 = FlashVLN(constants, properties, liquids=[liquid, liquid2], gas=gas)
res = flasher2.flash(T=T, P=P, zs=zs)
print('There are %s phases present at %f K and %f bar' % (res.phase_count, T, P/1e5))

#-------------------------------------------------------
# Задание 1-2-3
print('Задание 1-2-3:')

if res.VF > 0:
	print(res.gas.zs)
if res.VF == 1:  # Есть только пар
	print("Only vapour")
else:
	print("Liquid0: ")
	print(res.liquid0.zs)
	if res.liquid_count > 1:
			print("LIQUID PHASE SEPARATION")
			print("Liquid1: ")
			print(res.liquid1.zs)

#-------------------------------------------------------
# Задание 4
print('Задание 4:')

flasher = FlashVL(constants, properties, liquid=liquid, gas=gas)
res = flasher.flash(T=354, P=P, zs=[0.8, 0.2])

if res.VF > 0:
    print('Vapour exists')
    print(res.gas.zs)
if res.VF < 1:
    print('Liquid exists')
    print(res.liquid0.zs)

#-------------------------------------------------------
# Задание 5
print('Задание 5:')

T_exp = [355.05, 354.15, 353.65, 352.25, 351.95]
x_exp = [0.12400, 0.34800, 0.45550, 0.83350, 0.91500]
y_exp = [0.14100, 0.38450, 0.48500, 0.85800, 0.92950]

zs_calc_0 = [0.078, 0.771, 0.819, 0.878, 0.896]
zs_calc_1 = [1 - i for i in zs_calc_0]

x_liquid_calc = []
y_vapor_calc = []

for i in range(5):
	list_cals_xy = flasher.flash(T=T_exp[i], P=P, zs=[zs_calc_0[i], zs_calc_1[i]])
	# print('#' * 20)
	if list_cals_xy.VF > 0:
		# print(list_cals_xy.gas.zs)
		y_vapor_calc.append(list_cals_xy.gas.zs[0])
		# print('it is vapor')
	if list_cals_xy.VF < 1:
		# print(list_cals_xy.liquid0.zs)
		x_liquid_calc.append(list_cals_xy.liquid0.zs[0])
		# print('it is liquid')

print('Vapor mole fraction', y_vapor_calc)
print('Liquid mole fraction', x_liquid_calc)

print('\nErrors:')
error_x = sum([abs((x_exp[i] - x_liquid_calc[i]) / x_exp[i]) for i in range(5)]) / 5
error_y = sum([abs((y_exp[i] - y_vapor_calc[i]) / y_exp[i]) for i in range(5)]) / 5
print('Error for liquid mole fraction: ' + str(round(error_x * 10, 4)) + '%')
print('Error for vapor mole fraction: ' + str(round(error_y * 10, 4)) + '%')

# Построим график
# plt.plot(x_exp, y_exp)
# plt.plot(x_liquid_calc, y_vapor_calc)
# plt.scatter(x_exp, y_exp)
# plt.scatter(x_liquid_calc, y_vapor_calc)
#
# plt.show()