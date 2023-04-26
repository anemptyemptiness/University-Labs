clc
ezplot(@(x)x.^3-x, [-4,4]);
grid on;
title('Graph 1');

figure;
x=-2:0.01:2;
plot(x,sin(1 ./ x.^2 ), 'r');
grid on;
title('Graph 2');

figure;
ezplot(@(x)tan(x./2));
axis([-pi pi -10 10]);
grid on;
title('Graph 3');

figure;
ezplot(@(x)exp((-x.^2)./2), [-1.5, 1.5]);
hold on;
ezplot(@(x)x.^4 - x.^2, [-1.5, 1.5]);
grid on;
title('Graph 4,5');
matrix = [1 2 6 0 0;
        0 0 4 3 1;
        0 1 0 3 0;
        1 0 4 1 1;
        0 2 3 4 0;
        0 1 4 6 1];
syms Ca N O H P
elem = [Ca; N; O; H; P];
RANG = rank(matrix);
for n= 1:(5 - RANG)
    for m= 1:(6 - RANG)
        if det(matrix(n:n + RANG - 1,m:m + RANG - 1)) ~= 0
            matrix(n:n + RANG - 1,m:m + RANG - 1)
        end
    end
end

