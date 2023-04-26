clc; clear all, close all;

E = 1E-5;
e = 1E-10;

X0 = 0;
syms g;
 
y = (5*g.^2 - 3*exp(g+1) - 7);

func = @(x)double(subs(y,g,x));

dfunc = @(x)double(subs(diff(y),g,x));


y = (10*g-3*exp(g+1));

func0 = @(x)double(subs(y,g,x));
dfunc0 = @(x)double(subs(diff(y),g,x));
 
fprintf('Вариант 11\n\n');
fprintf('\n########################\n');

fprintf( '\n\nМетод половинного деления\n\n');
 
kol = 0;
 
val1 = -1000;
val2 = -val1;
 
 
while abs(val2-val1)>=E
x = (val1+val2)/2;
 
if func(x)==0
    val1=x;
    val2=x;
else
   if func(val1)*func(x)>0
      val1=x;
   else
      val2=x;
   end 
   kol = kol + 1;
end
end
 
x =(val1+val2)/2;
 
fprintf('Решение: %d\n',x);
fprintf('Итераций: %d\n',kol);
fprintf('Проверка: %d\n',func(x));
 
 
fprintf('\n########################\n');
 
fprintf('\n\nМетод перебора\n\n');
 
 
kol = 0;
x = -100;
step = 1;
 
 
while(abs(func(x+step)-func(x))>E&&kol<100)
  while(func(x)*func(x+step)>0)
    x = x+step;
  end
  step = step/2;
  kol = kol+1;
end
 
X = x;
 
fprintf('Решение: %d\n',x);
fprintf('Итераций: %d\n',kol);
fprintf('Проверка: %d\n',func(x+step));
fprintf('Погрешность: %d\n',abs(func(x+step)-func(x)));
 
fprintf('\n\n########################\n');
fprintf('\n\nМетод простой итерацeии\n\n');
 
fprintf('Проверка сходимости\n');
 
if(dfunc0(x)<1)
    fprintf('Вывод: в выбранной точке условие сходимости выполняется ( dx(%d) = %d [%d < 1]) , можно найти корень ур-я\n\n', x, dfunc0(x), dfunc0(x));
end
 
kol = 0;
x = X;
x0 = -x;
 
 
while(kol<100 && abs(func(x))>=e && abs(x0-x)>=e)
    x0 = x;
    x = func0(x);
    kol = kol+1;
end
 
 
fprintf('Решение: %d\n',x);
fprintf('Итераций: %d\n',kol);
fprintf('Проверка: %d\n',func(x));
fprintf('Погрешность решения: %d\n',abs((func(x))-0));
fprintf('Погрешность метода: %d\n',abs(func0(x)-x));
 
 
 
fprintf('\n########################\n');
fprintf('\n\nМетод секущих(метод хорд)\n\n');
 
kol = 0;
x = X;
val2 = -x;
val1 = x;
step = 1;
 
while(abs(val2 - val1) > e)
   val1 = val2 - (val2 - val1) * func(val2) / (func(val2) - func(val1));
   val2 = val1 + (val1 - val2) * func(val1) / (func(val1) - func(val2));
   kol = kol+1;
end
 
x = (val1+val2)/2;
fprintf('Решение: %d\n',x);
fprintf('Итераций: %d\n',kol);
fprintf('Проверка: %d\n',func(x));
fprintf('Погрешность: %d\n',abs(func(val1)-func(val2)));
 
 
 
fprintf('\n########################\n');
fprintf('\n\nМетод касательных\n\n');
 
kol = 0;
x = X;
x0 = x;
x = x0 - func(x0)/dfunc(x0);
 
 while(abs(x-x0)>e) 
    x0 = x;
    x = x0 - func(x0)/dfunc(x0);
    kol = kol+1;
 end
 
 
fprintf('Решение: %d\n',x);
fprintf('Итераций: %d\n',kol);
fprintf('Проверка: %d\n',func(x));
fprintf('Погрешность: %d\n',abs(func(x0)-func(x)));
 
fprintf('\n########################\n');
