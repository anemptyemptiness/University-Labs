from thermo import *
from thermo.unifac import DOUFSG, DOUFIP2016
# Load constants and properties
constants, properties = ChemicalConstantsPackage.from_IDs(['ethanol', '2-propanol'])
# Objects are initialized at a particular condition
T = 333.15
P = 1e5
zs = [.5, .5]

# Use Peng-Robinson for the vapor phase
eos_kwargs = {'Pcs': constants.Pcs, 'Tcs': constants.Tcs, 'omegas': constants.omegas}
gas = CEOSGas(PRMIX, HeatCapacityGases=properties.HeatCapacityGases, eos_kwargs=eos_kwargs)

# Configure the activity model
GE = UNIFAC.from_subgroups(chemgroups=constants.UNIFAC_Dortmund_groups, version=1, T=T, xs=zs,
						   interaction_data=DOUFIP2016, subgroups=DOUFSG)
# Configure the liquid model with activity coefficients
liquid = GibbsExcessLiquid(
	VaporPressures=properties.VaporPressures,
	HeatCapacityGases=properties.HeatCapacityGases,
	VolumeLiquids=properties.VolumeLiquids,
	GibbsExcessModel=GE,
	equilibrium_basis='Psat', caloric_basis='Psat',
	T=T, P=P, zs=zs)

# Create a flasher instance, assuming only vapor-liquid behavior
flasher = FlashVL(constants, properties, liquid=liquid, gas=gas)

# Create a T-xy plot at P bar
# _ = flasher.plot_Txy(P=P, pts=100)

# Create a P-xy plot at T Kelvin
# _ = flasher.plot_Pxy(T=T, pts=100)

# Create a xy diagram at T Kelvin
# _ = flasher.plot_xy(T=T, pts=100)

liquid2 = CEOSLiquid(PRMIX, eos_kwargs, HeatCapacityGases=properties.HeatCapacityGases, T=T, P=P, zs=zs)
flasher2 = FlashVLN(constants, properties, liquids=[liquid, liquid2], gas=gas)
res = flasher2.flash(T=T, P=P, zs=zs)
print('There are %s phases present at %f K and %f bar' %(res.phase_count,T,P/1e5))
if res.VF > 0:
	print(res.gas.zs)
if res.VF == 1:  # Есть только пар
	print("Only vapour")
else:
	print("Liquid0: ")
	print(res.liquid0.zs)
	if res.liquid_count>1:
			print("LIQUID PHASE SEPARATION")
			print("Liquid1: ")
			print(res.liquid1.zs)

#-------------------------------------------------------
# Задание 5
print('Задание 5:')

x_exp = [0.45550, 0.55200, 0.74250, 0.83350, 0.91500]
y_exp = [0.48500, 0.58950, 0.77000, 0.85800, 0.92950]
T_exp = [353.65, 353.35, 352.55, 352.25, 351.95]

zs_calc_0 = [0.1472, 0.2083, 0.4059, 0.4805, 0.5619]
zs_calc_1 = [1 - i for i in zs_calc_0]

x_liquid_calc = []
y_vapor_calc = []
for i in range(5):
	list_cals_xy = flasher2.flash(T=T_exp[i], P=P, zs=[zs_calc_0[i], zs_calc_1[i]])
	if list_cals_xy.VF > 0:
		y_vapor_calc.append(list_cals_xy.gas.zs[0])
	if list_cals_xy.VF < 1:
		x_liquid_calc.append(list_cals_xy.liquid0.zs[0])
	else:
		x_liquid_calc.append(0)

if (len(y_vapor_calc) < 5):
	while (len(y_vapor_calc) < 5):
		y_vapor_calc.append(0)

print('Vapor mole fractions: ', y_vapor_calc)
print('Liquid mole fractions: ', x_liquid_calc)

print('\nErrors:')
error_x = sum([abs((x_exp[i] - x_liquid_calc[i]) / x_exp[i]) for i in range(5)]) / 5
error_y = sum([abs((y_exp[i] - y_vapor_calc[i]) / y_exp[i]) for i in range(5)]) / 5
print('Error for liquid mole fraction: ' + str(round(error_x * 10, 4)) + '%')
print('Error for vapor mole fraction: ' + str(round(error_y * 10, 4)) + '%')