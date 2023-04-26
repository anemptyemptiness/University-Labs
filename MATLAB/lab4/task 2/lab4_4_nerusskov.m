clc;
A = [1 0 0.5; 0 1 0.5; 1 1 1];
F = [18.5; 13.5; 32];
E = 0.01;

V = diag(A); 
D = diag(V);
L = tril(A) - D;
U = triu(A) - D;



B = -(L + D)^-1*U;
F = (L + D)^-1*F;
fprintf('Норма B:');
disp(norm(B));


u = zeros(1,rank(B))';
u1 = u*B + F 
step = 0;
while (abs(max(u) - max(u1)) >= E)
    u = u1;
    u1 = B*u + F;
    step = step +1;
    if (step >10000)
        break;
    end
end
u = u1;
fprintf('Решение: \n');
disp(u')
step