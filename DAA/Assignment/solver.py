import math
import sympy as sp

def master_theorem(a, b, fn):
    """
    Applies Master Theorem for T(n) = aT(n/b) + f(n)
    """
    if b <= 1:
        return "Invalid value of b"

    log_ab = math.log(a) / math.log(b)  # log_b(a)
    
    # Define f(n) growth
    if 'n^' in fn:
        power = int(fn.split('n^')[1])  # Extract exponent from f(n) = n^d
    else:
        power = 0  # Constant function
    
    if power > log_ab:
        return f"T(n) = Θ(n^{power}) (Case 3: f(n) dominates)"
    elif power == log_ab:
        return f"T(n) = Θ(n^{power} log n) (Case 2: Balanced growth)"
    else:
        return f"T(n) = Θ(n^{log_ab}) (Case 1: Recursion dominates)"


def iterative_expansion(a, b, fn, base_case=1):
    """
    Expands T(n) = aT(n - b) + f(n) iteratively until base case is reached.
    """
    n = sp.Symbol('n')
    k = sp.Symbol('k')
    f_n = sp.sympify(fn)
    
    # Expansion: T(n) = aT(n - b) + f(n)
    T_n = base_case
    expansion = f_n
    i = 1
    while i < 10:  # Expand up to 10 steps for generalization
        expansion += a**i * f_n.subs(n, n - i * b)
        i += 1
    
    general_form = sp.summation(a**k * f_n.subs(n, n - k * b), (k, 0, (n-1)//b))
    
    return f"T(n) ≈ {general_form.simplify()}"


def solve_recurrence():
    print("Select the recurrence type:")
    print("1. Dividing Function (T(n) = aT(n/b) + f(n))")
    print("2. Decreasing Function (T(n) = aT(n - b) + f(n))")
    
    choice = int(input("Enter choice (1/2): "))
    
    if choice == 1:
        a = int(input("Enter value of a: "))
        b = int(input("Enter value of b: "))
        fn = input("Enter f(n) (e.g., 'n^2' for n^2, '1' for constant): ")
        
        print(master_theorem(a, b, fn))
    
    elif choice == 2:
        a = int(input("Enter value of a: "))
        b = int(input("Enter value of b: "))
        fn = input("Enter f(n) (e.g., 'n', '1', 'n^2'): ")
        
        print(iterative_expansion(a, b, fn))
    
    else:
        print("Invalid choice!")

# Run the solver
solve_recurrence()