function F = func6_nerusskov(x)

    F(1) = 1980 - 2 * x(1)^6 - x(2)^2;
    F(2) = 19 * x(1) + 66 * x(2) - 8 * exp(x(1)) + 9 * exp(x(2));

end
