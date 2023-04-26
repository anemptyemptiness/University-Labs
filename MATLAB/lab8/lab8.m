clc % очистка экрана

% данные
X_in = [3.33 7.47 9.18 10.48 13.97 15.74] % входные X
Y_in = [7.0568 26.4117 50.0166 78.7355 228.1487 363.1381] % входные Y
P_in = [0.3 0.7 1 0.7 0.8 0.2] % входные весовые коэффициенты

N = size(X_in,2) % количесвто точек

plot(X_in, Y_in, '*'); % отображение узловых точек ' * '
hold on;

% поиск оптимальной степени полинома
pol_pow = 2; % инициализация степени для поиска
pol_min = max(diff(Y_in,2)) - min(diff(Y_in,2)); % инициализация начального минимума
for i = 2:(N-2)
  cmp = max(diff(Y_in,i)) - min(diff(Y_in,i)); % разность между максимальным и минимальным
  if(cmp < pol_min)
    pol_min = cmp
    pol_pow = i
  end
end

pow_ = pol_pow % оптимальная степень полинома

% аппроксимация встроенным методом
mat_cffs = polyfit(X_in, Y_in, pol_pow) % поиск коэффициентов аппрокс. полинома степени pol_pow
mat_err = sum(abs(Y_in-polyval(mat_cffs,X_in))) % ошибка

% аппрокимация матрицей Вандермонда
V = vander(X_in); % матрица Вандермонда
V = V(1:N,(N-pol_pow):N) % удаляем "лишние" элементы для получения нужной степени
A = transpose(V)*V;
B = transpose(V)*transpose(Y_in);
v_cffs = transpose(inv(A)*B) % коэффициенты
v_err = sum(abs(Y_in-polyval(v_cffs,X_in))) % ошибка

% аппроксимация полинома fminsearch с учётом коэффициентов
a_p = v_cffs; % начальное приближение по раннее найденному(по заданию)
func_p = @(a, x) polyval(a, x); % функция аппроксимируемая
func_calc_p = @(a) sum(P_in.*(Y_in - func_p(a,X_in)).^2) % функция ошибок с учётом весов
min_cffs_p = fminsearch(func_calc_p,a_p) % коэффициенты fminsearch
min_p_err = sum(abs(Y_in-func_p(min_cffs_p,X_in))) % ошибка

% аппроксимация функцией fminsearch с учётом коэффициентов
a_s = [1 1 1]; % начальное приближение
func_s = @(a, x) a(1)*x.^a(2) + a(3); % функция аппроксимируемая f = a1 * x^a2 + a3
func_calc_s = @(a) sum(P_in.*(Y_in - func_s(a,X_in)).^2) % функция ошибок с учётом весов
min_cffs_s = fminsearch(func_calc_s,a_s) % коэффициенты fminsearch
min_s_err = sum(abs(Y_in-func_s(min_cffs_s,X_in))) % ошибка

% аппроксимация spap2
sp_coeff = spap2(3,4,X_in,Y_in,P_in) % коэффициенты spap2
sp_err = sum(abs(Y_in - fnval(sp_coeff,X_in))) % ошибка

X_new = X_in(1):0.01:X_in(end); % значения X для графиков

% Аппроксимация полиномом Чебышева
w = sqrt(P_in); % весовые коэффициенты
n = length(X_in) - 1; % степень полинома
x_range = linspace(min(X_in), max(X_in), 100); % диапазон для аппроксимации
cheb_nodes = cos(pi*(2*(0:n)+1)/(2*(n+1))); % узлы Чебышева
T = zeros(length(X_in), n+1); % матрица Чебышевских полиномов
T(:,1) = 1;
T(:,2) = X_in';
for k = 2:n
T(:,k+1) = 2*X_in'.*T(:,k) - T(:,k-1);
end
A = bsxfun(@times, w', T); % учет весовых коэффициентов
c_opt = A \ (w'.*Y_in'); % оптимальные коэффициенты
% вычисление аппроксимированных значений
T_approx = zeros(length(x_range), n+1); % матрица Чебышевских полиномов для диапазона
T_approx(:,1) = 1;
T_approx(:,2) = x_range';
for k = 2:n
T_approx(:,k+1) = 2*x_range'.*T_approx(:,k) - T_approx(:,k-1);
end
y_approx = T_approx * c_opt; % аппроксимированные значения

% построение графиков
xlabel('x');
ylabel('y');
plot(X_new,polyval(mat_cffs,X_new), 'red') % polyfit
plot(X_new,polyval(v_cffs,X_new), 'green') % Вандермонд
plot(X_new,fnval(sp_coeff,X_new),'blue') % spap2
plot(X_new,func_p(min_cffs_p,X_new), 'black') % fminsearch полиномов
plot(X_new,func_s(min_cffs_s,X_new), 'yellow') % fminsearch произвольной
plot(x_range, y_approx, 'magenta'); % Чебышёв

% подписи графиков
legend({'Data','polyfit', 'Вандермонд', 'spap2' , 'fminsearch полин.', 'fminsearch спец.', 'Аппроксимация полиномом Чебышева'}, 'Location', 'North')

