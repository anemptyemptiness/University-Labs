clc; clear all; 
x=[2, 2.5, 3, 3.5, 4, 4.5, 5, 5.5, 6];
y=[5.197, 7.78, 11.14, 15.09, 19.245, 23.11, 26.25, 28.6, 30.3];
x1=[3.75, 4.75, 5.25];
disp('�������� �������');
k=spline(x,y,x1);
disp(k);
plot(x,y,'b',x1,k,'sr')