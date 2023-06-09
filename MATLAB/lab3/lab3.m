clc
% ������� 1
disp('������� 1');
disp('');
disp('������� ������� �');
A = [6, -1, -1;
    1, -2, 3;
    3, 4, 4]
disp('����������� ������� �');
det(A)
disp('���� ������� �');
rank(A)
disp('����� ������� �');
norm(A)
disp('����� ��������������� ������� �');
cond(A)
disp('������ b');
b = [0; 1; -1]
disp('�������� ������� �');
inv(A) % ���������� �������� �������
disp('�������');
x = inv(A) * b
disp('������� � ������� linsolve(A, b)');
linsolve(A, b)
disp('�������� ��������');
b - A*x
disp('');
disp('');
% ������� 2
disp('������� 2');
disp('');
disp('������� ������� �');
A = [2, -1, 0, -3;
    1, -1, 0, 2
    3, -2, 1, 1;
    -1, 3, -1, 1]
disp('����������� ������� �');
det(A)
disp('���� ������� �');
rank(A)
disp('����� ������� �');
norm(A)
disp('����� ��������������� ������� �');
cond(A)
disp('������ b');
b = [-9; 8; -5; 9]
x = A \ b
disp('������� � ������� linsolve(A, b)');
linsolve(A, b)
disp('�������� ��������');
b - A*x
% ������� 3
disp('������� 3');
disp('');
% LU-����������
disp('������� ������� �');
A = [2.34, -1.42, -0.54, 0.21;
    1.44, -0.53, 1.43, -1.27;
    0.63, -1.32, -0.65, 1.43;
    0.56, 0.88, -0.67, -2.38]
disp('����������� ������� �');
det(A)
disp('���� ������� �');
rank(A)
disp('����� ������� �');
norm(A)
disp('����� ��������������� ������� �');
cond(A)
disp('������ b');
b = [0.66; -1.44; 0.94; 0.73]
disp('LU-���������� �� ������� L � U');
[L,U] = lu(A)
y = L \ b
x = U \ y
disp('������� � ������� linsolve(A, b');
linsolve(A, b)
disp('�������� �������');
b - A * x

% ������� 4
disp '������� �4';
chim = [1 2 6 0 0; % Ca(NO3)2
        0 0 4 3 1; % H3PO4
        0 1 0 3 0; % NH3
        1 0 4 1 1; % CaHPO4
        0 2 3 4 0; % NH4NO3
        0 1 4 6 1] % NH$H2PO4
A = rank(chim);
disp '�������� ������� � ������:';
disp (A);

% ��� ������ ����������
syms b13 b14 b15 b16 b23 b24 b25 b26
disp('������� � � ������������:');
B = [1 0 b13 b14 b15 b16;
    0 1 b23 b24 b25 b26]
disp('������� ��������� � b:');
umn = B * chim
umn = umn.';
[b13 b14 b15 b16] = solve(umn(:,1));
[b23 b24 b25 b26] = solve(umn(:,2));

B = [1 0 b13 b14 b15 b16;
     0 1 b23 b24 b25 b26;];
B = B.';
disp('������� ��� ������ ����������:');
disp(B);

% ��� ������ ����������
syms b12 b13 b14 b15 b22 b23 b24 b25;
disp('������� � � ������������:');
B = [1 b12 b13 b14 b15 0;
     0 b22 b23 b24 b25 1;]
disp('������� ��������� � b:');
umn = B*chim
umn = umn.';
[b12 b13 b14 b15] = solve(umn(:,1));
[b22 b23 b24 b25] = solve(umn(:,2));
 
B = [1 b12 b13 b14 b15 0;
     0 b22 b23 b24 b25 1;];
disp('������� ��� ������ ����������:');
B = B.';
disp(B);