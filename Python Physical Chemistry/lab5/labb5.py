import math

# Данные
T = 800 # К
dG1 = 15000 # Дж/моль
dG2 = -10000 # Дж/моль

# Задание 2 --> а)
# Расчет константы равновесия
R = 8.314 # Дж/моль*К
Ka_1 = math.exp(-dG1 / (R * T))
Ka_2 = math.exp(-dG2 / (R * T))
print("Задание 2 --> а)\nКонстанта равновесия Ka_1:", Ka_1)
print("Константа равновесия Ka_2:", Ka_2)

# Задание 2 --> б)
x = 0.26535655131594
y = 0.0279940281591725

CO = 0.1 - 2 * y - x
CO2 = y + x
C = y
H2O = 0.3 - x
H2 = x

print('\nЗадание 2 --> б)\nРавновесные концентрации всех веществ в системе:')
print('CO: ' + str(round(CO, 6)) + ' mol')
print('CO2: ' + str(round(CO2, 6)) + ' mol')
print('C: ' + str(round(C, 6)) + ' mol')
print('H2O: ' + str(round(H2O, 6)) + ' mol')
print('H2: ' + str(round(H2, 6)) + ' mol')

# Задание 2 --> в)
alpha = CO / 0.1
print('\nЗадание 2 --> в)\nРавновесная степень превращения CO:')
print(str(alpha))

T_enthalpy = 298.15 # K

def GetEnthalpy(EnthalpyC2H4, EnthalpyH2O, EnthalpyC2H5OH): #  (1 * C2H4 + 1 * H20) - 1 * C2H5OH
    return (EnthalpyC2H4 + EnthalpyH2O) - EnthalpyC2H5OH

EnthalpyC2H4 = 6.31426266E+03 * R
EnthalpyH2O = -241.826 * 1000
EnthalpyC2H5OH = -3.33765910E+04 * R

enthalpy_of_reaction = GetEnthalpy(EnthalpyC2H4, EnthalpyH2O, EnthalpyC2H5OH) # kJ/mol
print('\nТепловой эффект реакции: ' + str(round(enthalpy_of_reaction * (-1), 3) / 1000) + ' kJ/mol')