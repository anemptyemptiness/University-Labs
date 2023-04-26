import math

# Данные
T = 310 # К
dG1 = 6300 # Дж/моль
dG2 = 5300 # Дж/моль
c_acet = 0.2 # моль
c_isoprop = 13 # моль

# Задание 2 --> а)
# Расчет константы равновесия
R = 8.314 # Дж/моль*К
Ka_1 = math.exp(-dG1 / (R * T))
Ka_2 = math.exp(-dG2 / (R * T))
print("Задание 2 --> а)\nКонстанта равновесия Ka_1:", Ka_1)
print("Константа равновесия Ka_2:", Ka_2)

# Задание 2 --> б)
x = 0.029722
y = 0.020164

acetophenon_1 = 0.2 - y - x
izopropanol_1 = 13 - y - x
R_pheniletanol = y
aceton_1 = y + x
S_pheniletanol = x

print('\nЗадание 2 --> б)\nРавновесные концентрации всех веществ в системе:')
print('Ацетофенон: ' + str(round(acetophenon_1, 6)) + ' mol')
print('Изопропанол: ' + str(round(izopropanol_1, 6)) + ' mol')
print('R-фенилэтанол: ' + str(round(R_pheniletanol, 6)) + ' mol')
print('S-фенилэтанол: ' + str(round(S_pheniletanol, 6)) + ' mol')
print('Ацетон: ' + str(round(aceton_1, 6)) + ' mol')

# Задание 2 --> в)
alpha = aceton_1 / c_acet
print('\nЗадание 2 --> в)\nРавновесная степень превращения ацетофенона:')
print(str(alpha))

T_enthalpy = 298.15 # K

def GetEnthalpy(EnthalpyHydro, EnthalpyCarbon, EnthalpyEthen):
    return EnthalpyEthen - (2 * EnthalpyHydro + 2 * EnthalpyCarbon)

enthalpy_h2 = 0
enthalpy_c = 0
enthalpy_c2h4 =  round(6.31426266E+03 * R, 5)

enthalpy_of_reaction = GetEnthalpy(enthalpy_h2, enthalpy_c, enthalpy_c2h4) # kJ/mol
print('\nТепловой эффект реакции: ' + str(round(enthalpy_of_reaction * (-1), 3) / 1000) + ' kJ/mol')