clc
%№1
disp '№1';
disp '';
x = 2.5378;
y = 2.536;
dx = 0.0001;
dy = 0.001;
qx = 3.94E-05;
qy = 3.94E-04;
% Для сложения 
disp 'Сумма';
S1 = x + y;
disp(S1);
disp 'Погрешность абсолютная';
dX = dx + dy;
disp(dX);
disp 'Погрешность относительная';
qX = (x*qx + y*dy)./S1;
disp(qX);
% Для вычитания
disp 'Разница';
S2 = x - y;
disp(S2);
disp 'Погрешность абсолютная';
dX = dx + dy;
disp(dX);
disp 'Погрешность относительная';
qX = dX ./ abs(S2);
disp(qX);
%№2
disp '№2';
disp '';
x = 37.1;
y = 9.87;
z = 6.052;
dx = 0.1;
dy = 0.05;
dz = 0.02;
disp 'u(x,y,z) = x^2*y^2/z^4';
u = x.^2*y.^2./z^4;
disp 'Абсолютная погрешность';
D = abs((2*x*(y.^2)./z.^4)*dx) + abs(((x.^2)*2*y./z.^4)*dy)+abs((-4*(x.^2)*(y.^2)./z.^5)*dz);
disp(D);
disp 'Относительная погрешность';
%Q = 2*(dx./x) + 2*(dy./y) - 4*(dz./z);
dQ = D/abs(u);
disp(dQ);
%№3
disp '№3';
disp '';
x = -3.59;
y = 0.467;
z = 563.2;
dx = 0.01;
dy = 0.001;
dz = 0.1;
disp 'f(x, y, z) = x*sin(y) + z^(1/3)';
u = x*sin(y) + z.^(1/3);
disp 'Абсолютная погрешность';
D = abs(sin(y)*dx)+abs(x*cos(y)*dy)+abs((1./3)*z.^(-2./3)*dz);
disp(D);
disp 'Относительная погрешность';
%Q = ((x+y)*(dx./x+dy./y) + z*qy)./(x*sin(y) + z.^(1/3));
dQ = D / abs(u);
disp(dQ);
%№4 - часть 1
disp '№4 - часть 1';
disp '';
% искомая матрица
matrix = [1 2 6 0 0;
        0 0 4 3 1;
        0 1 0 3 0;
        1 0 4 1 1;
        0 2 3 4 0;
        0 1 4 6 1]
syms Ca N O H P
% матрица элементов
elem = [Ca; N; O; H; P]
% ранг матрицы
RANG = rank(matrix)
% две подматрицы (лаб 2.1)
for n = 1:(5 - RANG)
    for m = 1:(6 - RANG)
        if det(matrix(n:n + RANG - 1,m:m + RANG - 1)) ~= 0
            matrix(n:n + RANG - 1,m:m + RANG - 1)
        end
    end
end

%№4 - часть 2
disp '№4 - часть 2';
disp '';
j = rank(matrix);
k = rank(matrix);
count = 0;
for n = 1:(5 - RANG + 2)
    for m = 1:(6 - RANG)
        if det(matrix(n:n + RANG - 1,m:m + RANG - 1)) ~= 0
            matrix(n:n + RANG - 1,m:m + RANG - 1)
            det(matrix(n:n + RANG - 1,m:m + RANG - 1))
            count = count + 1;
        end
    end
end
disp 'Количество невырожденных матриц';
disp(count);
    





















