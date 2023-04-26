clc
clearvars

%Данные
x_data = [1.15 5.35 7.41 11.39 13.12 16.46] %входные X
y_data = [1.3133 4.1311 11.8761 61.0353 106.8974 265.0287] %входные Y
x_ref = [10.35 14.77 11.44]
x_solve = 0.5;
eps = 1e-2

f = @(x) 12*(1-exp(-0.05*x));

%Табличная функция
poly_got_m = polyfit(x_data, y_data, size(y_data,2)-1) %полином табличной функции
poly_derivative1 = polyder(poly_got_m) %коэффициенты первой производной
poly_derivative2 = polyder(poly_derivative1) %коэффициенты второй производной
y_der1_polyder = polyval(poly_derivative1,x_ref) %точки первой производной
y_der2_polyder = polyval(poly_derivative2,x_ref) %точки второй производной

%графики табличной функци и производных
X_new_poly = x_data(1):0.01:x_data(end);
figure(1);
hold on;
plot(x_data,y_data,'*','Color','k');
plot(X_new_poly,polyval(poly_got_m,X_new_poly));
plot(X_new_poly,polyval(poly_derivative1,X_new_poly));
plot(X_new_poly,polyval(poly_derivative2,X_new_poly));
title('Table function');
xlabel('X');
ylabel('Y');
legend({'Data','Function', 'First derivative', 'Second derivative'},'Location','northwest');

%функции простых производных
f_diff = @(x,f_in,eps) (f_in(x+eps)-f_in(x))./eps; %функция первой простой производной
f_diff2 = @(x,f_in,eps) (f_diff(x+eps,f_in,eps)-f_diff(x,f_in,eps))./eps; %функция второй простой производной

%функции многоточечных производных
f_diff_multi = @(x,f_in,eps) (f_in(x-2*eps)-8*f_in(x-eps)+8*f_in(x+eps)-f_in(x+2*eps))./(12*eps); %функция первой многоточечной производной
f_diff_multi2 = @(x,f_in,eps) (f_diff_multi(x-2*eps,f_in,eps)-8*f_diff_multi(x-eps,f_in,eps)+...
8*f_diff_multi(x+eps,f_in,eps)-f_diff_multi(x+2*eps,f_in,eps))./(12*eps); %функция второй многоточечной производной

%расчёт производных функции в точке
y1_simple_func = f_diff(x_solve,f,eps) %расчёт первой простой производной
y2_simple_func = f_diff2(x_solve,f,eps) %расчёт второй простой производной

y1_polydot_func = f_diff_multi(x_solve,f,eps) %расчёт первой многоточечной производной
y2_polydot_func = f_diff_multi2(x_solve,f,eps) %расчёт второй многоточечной производной

%аналитические производные через symbolic
sym_diff1 = simplify(diff(sym(f),1)) %первая аналитическая производная
sym_diff2 = simplify(diff(sym(f),2)) %вторая аналитическая производная
diff1_sym = matlabFunction(sym_diff1); %первая аналитическая производная matlab
diff2_sym = matlabFunction(sym_diff2); %вторая аналитическая производная matlab

%расчёт аналитических значений
actual_1 = diff1_sym(x_solve) %аналитическое значение первой произвольной
actual_2 = diff2_sym(x_solve) %аналитическое значение второй произвольной

%расчёт ошибок в точке
err1_simple = abs(y1_simple_func-actual_1) %ошибка первой простой производной
err1_multi = abs(y1_polydot_func-actual_1) %ошибка первой многоточечной производной
err2_simple = abs(y2_simple_func-actual_2) %ошибка второй простой производной
err2_multi = abs(y2_polydot_func-actual_2) %ошибка второй многоточечной производной

%график зависимости ошибки первой производной от величины шага
err_func = @(f,f_d,h,x,act) abs(f_d(x,f,h)-act(x)); %функция ошибки
Hx = 0.5:-1e-5:1e-5;
figure(2);
plot(Hx,err_func(f,f_diff,Hx,x_solve,diff1_sym));
hold on;
plot(Hx,err_func(f,f_diff_multi,Hx,x_solve,diff1_sym));
title('First derivative error');
xlabel('step');
ylabel('error');
legend({'Single-dot','Multi-dot'},'Location','northwest');

%график зависимости ошибки второй производной от величины шага
figure(3);
plot(Hx,err_func(f,f_diff2,Hx,x_solve,diff2_sym));
hold on;
plot(Hx,err_func(f,f_diff_multi2,Hx,x_solve,diff2_sym));
title('Second derivative error');
xlabel('step');
ylabel('error');
legend({'Single-dot','Multi-dot'},'Location','northwest');

%график абсолютных ошибок первой производной
X_new = -1:0.01:1;
figure(4);
hold on;
plot(X_new,err_func(f,f_diff,eps,X_new,diff1_sym));
plot(X_new,err_func(f,f_diff_multi,eps,X_new,diff1_sym));
title('First derivative absolute error');
xlabel('X');
ylabel('Y');
legend({'Single-dot error', 'Multi-dot error'},'Location','northwest');

%график абсолютных ошибок первой производной
X_new = -1:0.01:1;
figure(5);
hold on;
plot(X_new,err_func(f,f_diff2,eps,X_new,diff2_sym));
plot(X_new,err_func(f,f_diff_multi2,eps,X_new,diff2_sym));
title('Second derivative absolute error');
xlabel('X');
ylabel('Y');
legend({'Single-dot error', 'Multi-dot error'},'Location','northwest');
