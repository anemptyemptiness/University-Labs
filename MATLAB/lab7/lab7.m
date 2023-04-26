clc; 
clear;

x = [1.15 5.35 7.41 11.39 13.12 16.46];
y = [1.3133 4.1311 11.8761 61.0353 106.8974 265.0287];
xx = linspace(1, 17);

% наиболее подходящая степень
d = y;
mn = zeros(1, 5);

for i = 1 : 5
   d = diff(d);
   mn(i) = (max(d) - min(d)); 
end

min = mn(1);
min_j = 1;

for j = 1 : 5
   if mn(j) < min
       min = mn(j);
       min_j = j;
   end;
end

disp('значения d таблицы конечных разностей');
disp(mn);
disp('наиболее удовлетворительная степень полинома');
disp(min_j);

% с помощью матрицы Вандернонда
W = vander(x');
pW = W \ y';
if(det(W) ~= 0) 
    disp('Коэфиценты, определённые с помощью матрицы Вандернонда:');
    disp(pW);
    yW = polyval(pW, xx);

    subplot(1, 3, 1), plot(xx, yW);
    title('Вандернонд');
    hold on
    scatter(x, y, 'p');
end;


% стандартными функциями MatLab
p = polyfit(x, y, min_j);
disp('Коэфиценты, определённые стандартными функциями MatLab:');
disp(p);
yy = polyval(p, xx);

subplot(1, 3, 2), plot(xx, yy);
title('MatLab');
hold on
scatter(x, y, 'p');


% с помощью сплайнов
yS= interp1(x, y, xx, 'spline');

subplot(1, 3, 3), plot(xx, yS);
title('сплайны');
hold on
scatter(x, y, 'p');


