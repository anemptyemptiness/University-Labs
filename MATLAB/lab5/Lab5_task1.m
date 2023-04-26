clear, clc
x = -4.0 : 0.0001 : 4.0;
e = 0.0001;
p = x - x.^2 + 1;
disp('Встроенная функция fzero');
x0 = fzero('(x - x.^2 + 1)', [-1 0])
x1 = fzero('(x - x.^2 + 1)', [1 2])
plot(x,p); grid on; hold on; 
plot(x0,0,'r*');
plot(x1,0,'r*');
syms t;
p = t - t.^2 + 1;
f = @(x)double(subs(p,t,x));
fprintf( 'Проверка х0: %d\n',f(x0));
fprintf( 'Проверка х1: %d\n',f(x1));



disp('Метод перебора');
f = inline('(x - x.^2 + 1)');
h = 0.0001; 
eps = 10e-4;
x = -2; % начальное приближение 
x1 = 0.0;
count = 0; % счетчик итераций 
disp('Корень 1');
while ((f(x)*f(x+h)) > 0)
    x = x + h;
%     if f(x) == 0
    if abs(f(x)) <=eps
        x_ans = x;
        disp(x_ans);
     
    end
    count = count + 1;
end
disp('Корень 2');
while ((f(x1)*f(x1+h)) > 0)
    x1 = x1 + h;
%     if f(x) == 0
    if abs(f(x1)) <=eps
        x_ans = x1;
        disp(x_ans);
    end
    count = count + 1;
end
fprintf('Количество итераций %.1f\n\n', count);

disp('Метод касательных');
syms t;
p = t - t.^2 + 1;
f = @(x)double(subs(p,t,x));
df = @(x)double(subs(diff(p),t,x));

counter = 0;

x0 = 4;
x = x0 - f(x0)/df(x0);
 
 while(abs(x-x0)>e) 
    x0 = x;
    x = x0 - f(x0)/df(x0);
    counter = counter+1;
 end
 
 
fprintf( 'Корень 1: %d\n',x);
fprintf( 'Количество Итераций: %d\n',counter);
fprintf( 'Проверка: %d\n',f(x));
fprintf( 'Погрешность: %d\n\n',abs(f(x0)-f(x)));

x0 = 0;
x = x0 - f(x0)/df(x0);
counter = 0;
 while(abs(x-x0)>e) 
    x0 = x;
    x = x0 - f(x0)/df(x0);
    counter = counter+1;
 end
fprintf( 'Корень 2: %d\n',x);
fprintf( 'Количество Итераций: %d\n',counter);
fprintf( 'Проверка: %d\n',f(x));
fprintf( 'Погрешность: %d\n',abs(f(x0)-f(x)));

