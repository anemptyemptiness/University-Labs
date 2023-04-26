clc;
A = [1 0 0.5; 0 1 0.5; 1 1 1];
F = [18.5; 13.5; 32];
e = 0.0001;

F = A' * F;
A = A' * A;

V = diag(A);
D = diag(V);
LL = tril(A);
L = LL - D;
UU = triu(A);
U = UU - D;


u = [0; 0; 0];
u1 = u;

step = 1;
% решение методом Зейделя
while true
    u1 = -(L+D)\U*u+(L+D)\F;
    if abs(norm(u1) - norm(u)) <= e
        break;
    end
    u = u1;
    step = step + 1;
    if (step >10000)
        break;
    end
end
u = u1
fprintf('Точнось и количество шагов:\n');
accuracy =  norm(A * u - F)
step