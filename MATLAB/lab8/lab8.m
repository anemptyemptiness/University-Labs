clc % ������� ������

% ������
X_in = [3.33 7.47 9.18 10.48 13.97 15.74] % ������� X
Y_in = [7.0568 26.4117 50.0166 78.7355 228.1487 363.1381] % ������� Y
P_in = [0.3 0.7 1 0.7 0.8 0.2] % ������� ������� ������������

N = size(X_in,2) % ���������� �����

plot(X_in, Y_in, '*'); % ����������� ������� ����� ' * '
hold on;

% ����� ����������� ������� ��������
pol_pow = 2; % ������������� ������� ��� ������
pol_min = max(diff(Y_in,2)) - min(diff(Y_in,2)); % ������������� ���������� ��������
for i = 2:(N-2)
  cmp = max(diff(Y_in,i)) - min(diff(Y_in,i)); % �������� ����� ������������ � �����������
  if(cmp < pol_min)
    pol_min = cmp
    pol_pow = i
  end
end

pow_ = pol_pow % ����������� ������� ��������

% ������������� ���������� �������
mat_cffs = polyfit(X_in, Y_in, pol_pow) % ����� ������������� �������. �������� ������� pol_pow
mat_err = sum(abs(Y_in-polyval(mat_cffs,X_in))) % ������

% ������������ �������� �����������
V = vander(X_in); % ������� �����������
V = V(1:N,(N-pol_pow):N) % ������� "������" �������� ��� ��������� ������ �������
A = transpose(V)*V;
B = transpose(V)*transpose(Y_in);
v_cffs = transpose(inv(A)*B) % ������������
v_err = sum(abs(Y_in-polyval(v_cffs,X_in))) % ������

% ������������� �������� fminsearch � ������ �������������
a_p = v_cffs; % ��������� ����������� �� ������ ����������(�� �������)
func_p = @(a, x) polyval(a, x); % ������� ����������������
func_calc_p = @(a) sum(P_in.*(Y_in - func_p(a,X_in)).^2) % ������� ������ � ������ �����
min_cffs_p = fminsearch(func_calc_p,a_p) % ������������ fminsearch
min_p_err = sum(abs(Y_in-func_p(min_cffs_p,X_in))) % ������

% ������������� �������� fminsearch � ������ �������������
a_s = [1 1 1]; % ��������� �����������
func_s = @(a, x) a(1)*x.^a(2) + a(3); % ������� ���������������� f = a1 * x^a2 + a3
func_calc_s = @(a) sum(P_in.*(Y_in - func_s(a,X_in)).^2) % ������� ������ � ������ �����
min_cffs_s = fminsearch(func_calc_s,a_s) % ������������ fminsearch
min_s_err = sum(abs(Y_in-func_s(min_cffs_s,X_in))) % ������

% ������������� spap2
sp_coeff = spap2(3,4,X_in,Y_in,P_in) % ������������ spap2
sp_err = sum(abs(Y_in - fnval(sp_coeff,X_in))) % ������

X_new = X_in(1):0.01:X_in(end); % �������� X ��� ��������

% ������������� ��������� ��������
w = sqrt(P_in); % ������� ������������
n = length(X_in) - 1; % ������� ��������
x_range = linspace(min(X_in), max(X_in), 100); % �������� ��� �������������
cheb_nodes = cos(pi*(2*(0:n)+1)/(2*(n+1))); % ���� ��������
T = zeros(length(X_in), n+1); % ������� ����������� ���������
T(:,1) = 1;
T(:,2) = X_in';
for k = 2:n
T(:,k+1) = 2*X_in'.*T(:,k) - T(:,k-1);
end
A = bsxfun(@times, w', T); % ���� ������� �������������
c_opt = A \ (w'.*Y_in'); % ����������� ������������
% ���������� ������������������ ��������
T_approx = zeros(length(x_range), n+1); % ������� ����������� ��������� ��� ���������
T_approx(:,1) = 1;
T_approx(:,2) = x_range';
for k = 2:n
T_approx(:,k+1) = 2*x_range'.*T_approx(:,k) - T_approx(:,k-1);
end
y_approx = T_approx * c_opt; % ������������������ ��������

% ���������� ��������
xlabel('x');
ylabel('y');
plot(X_new,polyval(mat_cffs,X_new), 'red') % polyfit
plot(X_new,polyval(v_cffs,X_new), 'green') % ����������
plot(X_new,fnval(sp_coeff,X_new),'blue') % spap2
plot(X_new,func_p(min_cffs_p,X_new), 'black') % fminsearch ���������
plot(X_new,func_s(min_cffs_s,X_new), 'yellow') % fminsearch ������������
plot(x_range, y_approx, 'magenta'); % �������

% ������� ��������
legend({'Data','polyfit', '����������', 'spap2' , 'fminsearch �����.', 'fminsearch ����.', '������������� ��������� ��������'}, 'Location', 'North')

