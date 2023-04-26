clc;
clear;

%Графики функций
ezplot('2*x^6+y^2=1980');
hold on;
ezplot('8*exp(x)-9*exp(y)=19*x+66*y');
grid on;


%Решение fsolve
u1 = fsolve(@func6_nerusskov, [0 0])
plot(u1(1), u1(2), 'r.');

%метод простых итераций (синий)
disp('метод простых итераций');
e = 0.00001;
u = [0 0];
u1 = u;
i = 0;
syms x y;
fsimple = [1980-2*x^6-y^2 19*x+66*y-8*exp(x)-9*exp(y)];
v = [x y];
R = jacobian(fsimple,v)
disp('Норма = ')
disp(vpa(norm(subs(R, v, u))));

while true
   if (norm(subs(R, v, u))) > 1
       disp('не сходится');
       break
   end
   i = i + 1;
   u1 = vpa(subs(fsimple, v, u));
   if norm(abs(u1 - u)) <= e
       break
   end
   u = u1;
   if (i>10000)
       break
   end
end

u = u1
i
p1 = plot(u(1), u(2), 'bp');
set(p1, 'MarkerSize', 15);

%метод Ньютона (красный)
disp('метод Ньютона');
u = [4 2];
u1 = u;
i = 0;
R = jacobian(func6_nerusskov([x y]),v)

while true
   W = (subs(R, v, u));
   if det(W) >= 1
       disp('не сходится');
       break
   end
   i = i + 1;
   du = -inv(W)*func6_nerusskov(u)';
   u1 = u + du';
   if norm(abs(u1 - u)) <= e
       break
   end
   u = u1;
   if (i>10000)
       break
   end
end

u = u1
i
p2 = plot(u(1), u(2), 'rp');
set(p2, 'MarkerSize', 15);