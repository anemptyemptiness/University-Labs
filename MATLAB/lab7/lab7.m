clc; 
clear;

x = [1.15 5.35 7.41 11.39 13.12 16.46];
y = [1.3133 4.1311 11.8761 61.0353 106.8974 265.0287];
xx = linspace(1, 17);

% �������� ���������� �������
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

disp('�������� d ������� �������� ���������');
disp(mn);
disp('�������� ������������������ ������� ��������');
disp(min_j);

% � ������� ������� �����������
W = vander(x');
pW = W \ y';
if(det(W) ~= 0) 
    disp('����������, ����������� � ������� ������� �����������:');
    disp(pW);
    yW = polyval(pW, xx);

    subplot(1, 3, 1), plot(xx, yW);
    title('����������');
    hold on
    scatter(x, y, 'p');
end;


% ������������ ��������� MatLab
p = polyfit(x, y, min_j);
disp('����������, ����������� ������������ ��������� MatLab:');
disp(p);
yy = polyval(p, xx);

subplot(1, 3, 2), plot(xx, yy);
title('MatLab');
hold on
scatter(x, y, 'p');


% � ������� ��������
yS= interp1(x, y, xx, 'spline');

subplot(1, 3, 3), plot(xx, yS);
title('�������');
hold on
scatter(x, y, 'p');


