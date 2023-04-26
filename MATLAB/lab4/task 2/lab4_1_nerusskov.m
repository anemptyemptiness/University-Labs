clc;
A = [1 0 0.5; 1 1 1; 0 1 0.5];
F = [18.5; 32; 13.5];
e = 0.0001;
N = 3;
disp(linsolve(A, F));

%детерминант, ранг, норма и число обусловленности
detA =  det(A)
rankA = rank(A)
normA =  norm(A)
condA = cond(A)

% Решение методом простых итераций
u = [0; 0; 0];
u1 = u;
k = 0.01;
A1 = eye(N) - k * (A' * A);
F1 = k * (A' * F);
eig(A1)
step = 1;
while true
    u1 = A1 * u + F1;
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
accuracy = norm(A * u - F)
step