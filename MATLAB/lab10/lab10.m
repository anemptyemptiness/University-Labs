clc
clearvars

fun = @(t) (12).*(1-exp((-0.05).*t))

Et = 1e-2
Es = 1e-4

a = -11
b = -5.5

%������ �������
figure(1);
X_new = a:0.01:b;
Y_new = fun(X_new);
plot(X_new, Y_new);
grid on;
title('function');
xlabel('X');
ylabel('Y');

%������������
real_int = matlabFunction(int(sym(fun))) %������� ��������������� �������
real_ans = real_int(b) - real_int(a) %������� �������� �����

%������������ ���� ��� ������ ������� ���������
snormaliz = @(x,d) x./ceil(x./d);
snormaliz_smp = @(x,d) x./(ceil(x./d)+mod(ceil(x./d),2)); %������� ������� ������ ����� ����������

%������� ������ ��������
trpz_impl = @(fun,a,b,h) h.*((0.5).*fun(a)+sum(fun((a+h):h:(b-h)))+(0.5).*fun(b));

%������� �
call = @(f,m) f(m);
M_p = @(fi,ai,bi,ei,p) max(abs(call(matlabFunction(diff(sym(fi),p)),(ai:ei:bi))));

%������� ��� ������� ���� ��������
h_t = @(fi,ai,bi,ei) snormaliz((b-a),sqrt(12*ei./((bi-ai).*M_p(fi,ai,bi,ei,2))));

%������ �� ������ ��������
step_t = h_t(fun,a,b,Et) %������� ���
ans_t = trpz_impl(fun,a,b,step_t) %������� ��������
err_tr = abs(ans_t - real_ans) %������ � ��������� � �������������

%������� ��������� ���������� �����
I = @(I,I2,p) I2 + (I2-I)./((2).^p-1);

%��������� ������ ��������
trapz_h1 = trpz_impl(fun,a,b,step_t);  %������ � ����� �����
trapz_h2 = trpz_impl(fun,a,b,step_t/2); %������ � ���������� �����
I_trapz = I(trapz_h1,trapz_h2,2) %���������
err_Itr = abs(I_trapz - real_ans) %������ � ��������� � �������������

%������� ������ ��������
smps_impl = @(fun,a,b,h) (h/3)*(fun(a)+fun(b)+4*sum(fun((a+h):(2*h):(b-h)))+2*sum(fun((a+2*h):(2*h):(b-h))));

%������� ��� ������� ���� ��������
h_s = @(fi,ai,bi,ei) snormaliz_smp((b-a),(180*ei/((bi-ai)*M_p(fi,ai,bi,ei,4))).^(1/4));

%������ ��������
step_s = h_s(fun,a,b,Es) %������� ���
ans_s = smps_impl(fun,a,b,step_s) %������� ��������
err_s = abs(ans_s - real_ans) %������ � ��������� � �������������

%���������� �����
ans_v = quad(fun,a,b)
err_v = abs(ans_v - real_ans) %������ � ��������� � �������������

%������� ������
figure(2); %������ ������ ��������
X_new = (b-a)./(100:1:1000); %������ �����
Y_new = arrayfun(@(step) trpz_impl(fun,a,b,step), X_new); %������ �������� �� �����
plot(X_new,abs(real_ans - Y_new)); %����� �������
xlabel('step');
ylabel('error');
title('Trapeze method')
grid on;

figure(3); %������ ������ �������� � ���������� �����
X_new = (b-a)./(100:1:1000); %������ �����
Y_new = arrayfun(@(step) trpz_impl(fun,a,b,step), X_new); %������ �������� �� �����
Y_new2 = arrayfun(@(step) trpz_impl(fun,a,b,step/2), X_new); %������ �������� �� �����
plot(X_new,abs(real_ans - I(Y_new,Y_new2,2))); %����� �������
xlabel('step');
ylabel('error');
title('Trapeze method')
grid on;

figure(4); %������ ������ ��������
X_new = (b-a)./(100:2:1000); %������ �����
Y_new = arrayfun(@(step) smps_impl(fun,a,b,step), X_new); %������ �������� �� �����
plot(X_new,abs(real_ans - Y_new)); %����� �������
xlabel('step');
ylabel('error');
title('Simpsons method')
grid on;

%����� 2 � �����
syms X A P real positive
simplify(A^X*exp(-X))
int(A^X*exp(-X),X)

%����� 3 � �����
simplify((1+X)/(X+A)^(P+1))
sym_int = simplify(int((1+X)/((X+A)^(P+1)),X))
sym_int_d = simplify(int((1+X)/((X+A)^(P+1)),X,0,inf))
