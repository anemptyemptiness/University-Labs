clc
clearvars

fun = @(t) (12).*(1-exp((-0.05).*t))

Et = 1e-2
Es = 1e-4

a = -11
b = -5.5

%график функции
figure(1);
X_new = a:0.01:b;
Y_new = fun(X_new);
plot(X_new, Y_new);
grid on;
title('function');
xlabel('X');
ylabel('Y');

%аналитически
real_int = matlabFunction(int(sym(fun))) %находим интегрированную функцию
real_ans = real_int(b) - real_int(a) %находим истинный ответ

%нормализация шага для целого деления интервала
snormaliz = @(x,d) x./ceil(x./d);
snormaliz_smp = @(x,d) x./(ceil(x./d)+mod(ceil(x./d),2)); %Симпсон требует чётное число интервалов

%функция метода трапеций
trpz_impl = @(fun,a,b,h) h.*((0.5).*fun(a)+sum(fun((a+h):h:(b-h)))+(0.5).*fun(b));

%функция М
call = @(f,m) f(m);
M_p = @(fi,ai,bi,ei,p) max(abs(call(matlabFunction(diff(sym(fi),p)),(ai:ei:bi))));

%функции для расчёта шага трапеций
h_t = @(fi,ai,bi,ei) snormaliz((b-a),sqrt(12*ei./((bi-ai).*M_p(fi,ai,bi,ei,2))));

%расчёт по методу трапеций
step_t = h_t(fun,a,b,Et) %находим шаг
ans_t = trpz_impl(fun,a,b,step_t) %находим значение
err_tr = abs(ans_t - real_ans) %ошибка в сравнение с аналитическим

%функция уточнение процедурой Рунге
I = @(I,I2,p) I2 + (I2-I)./((2).^p-1);

%уточнение метода трапеций
trapz_h1 = trpz_impl(fun,a,b,step_t);  %расчёт с целым шагом
trapz_h2 = trpz_impl(fun,a,b,step_t/2); %расчёт с половинным шагом
I_trapz = I(trapz_h1,trapz_h2,2) %уточнение
err_Itr = abs(I_trapz - real_ans) %ошибка в сравнение с аналитическим

%функция метода симпсона
smps_impl = @(fun,a,b,h) (h/3)*(fun(a)+fun(b)+4*sum(fun((a+h):(2*h):(b-h)))+2*sum(fun((a+2*h):(2*h):(b-h))));

%функции для расчёта шага Симпсона
h_s = @(fi,ai,bi,ei) snormaliz_smp((b-a),(180*ei/((bi-ai)*M_p(fi,ai,bi,ei,4))).^(1/4));

%расчёт симпсона
step_s = h_s(fun,a,b,Es) %находим шаг
ans_s = smps_impl(fun,a,b,step_s) %находим значение
err_s = abs(ans_s - real_ans) %ошибка в сравнение с аналитическим

%встроенный метод
ans_v = quad(fun,a,b)
err_v = abs(ans_v - real_ans) %ошибка в сравнение с аналитическим

%графики ошибок
figure(2); %график метода трапеций
X_new = (b-a)./(100:1:1000); %расчёи шагов
Y_new = arrayfun(@(step) trpz_impl(fun,a,b,step), X_new); %расчёт значений по шагам
plot(X_new,abs(real_ans - Y_new)); %вывод разницы
xlabel('step');
ylabel('error');
title('Trapeze method')
grid on;

figure(3); %график метода трапеций с уточнением Рунге
X_new = (b-a)./(100:1:1000); %расчёи шагов
Y_new = arrayfun(@(step) trpz_impl(fun,a,b,step), X_new); %расчёт значений по шагам
Y_new2 = arrayfun(@(step) trpz_impl(fun,a,b,step/2), X_new); %расчёт значений по шагам
plot(X_new,abs(real_ans - I(Y_new,Y_new2,2))); %вывод разницы
xlabel('step');
ylabel('error');
title('Trapeze method')
grid on;

figure(4); %график метода Симпсона
X_new = (b-a)./(100:2:1000); %расчёи шагов
Y_new = arrayfun(@(step) smps_impl(fun,a,b,step), X_new); %расчёт значений по шагам
plot(X_new,abs(real_ans - Y_new)); %вывод разницы
xlabel('step');
ylabel('error');
title('Simpsons method')
grid on;

%номер 2 с сайта
syms X A P real positive
simplify(A^X*exp(-X))
int(A^X*exp(-X),X)

%номер 3 с сайта
simplify((1+X)/(X+A)^(P+1))
sym_int = simplify(int((1+X)/((X+A)^(P+1)),X))
sym_int_d = simplify(int((1+X)/((X+A)^(P+1)),X,0,inf))
