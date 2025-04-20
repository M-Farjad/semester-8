# Recurrence Solver - Python Version
# Author: [Farjad Waseem/Group - G13]
# CSC-208 Design and Analysis of Algorithms

import math

# ------------------------ Helper Methods ------------------------
def get_log_base(base, val):
    return math.log(val) / math.log(base)

def get_fn_class(fn):
    """
    Simple parser for f(n). Should return class like n, n^2, n log n, etc.
    You can improve this further.
    """
    fn = fn.replace(" ", "").lower()
    if "nlogn" in fn:
        return "nlogn"
    elif "logn" in fn:
        return "logn"
    elif "n^2" in fn or "n**2" in fn:
        return "n^2"
    elif "n^" in fn or "n**" in fn:
        power = fn.split("^" if "^" in fn else "**")[1]
        return f"n^{power}"
    elif fn == "n":
        return "n"
    elif fn == "1":
        return "1"
    else:
        return "unknown"

# --------------------- Master Theorem Solver ---------------------
def master_theorem(a, b, fn):
    fn_class = get_fn_class(fn)
    log_b_a = get_log_base(b, a)

    print(f"\n[INFO] log_b(a) = log_{b}({a}) = {log_b_a:.2f}")
    print(f"[INFO] f(n) is in class: {fn_class}")

    if fn_class == "n":
        if log_b_a < 1:
            return f"Θ(n)"
        elif log_b_a == 1:
            return f"Θ(n log n)"
        else:
            return f"Θ(n^{log_b_a:.2f})"

    elif fn_class == "n^2":
        if log_b_a < 2:
            return f"Θ(n^2)"
        elif log_b_a == 2:
            return f"Θ(n^2 log n)"
        else:
            return f"Θ(n^{log_b_a:.2f})"

    elif fn_class == "nlogn":
        if math.isclose(log_b_a, 1, abs_tol=0.1):
            return f"Θ(n log^2 n)"
        elif log_b_a < 1:
            return f"Θ(n log n)"
        else:
            return f"Θ(n^{log_b_a:.2f})"

    elif fn_class == "1":
        return f"Θ(n^{log_b_a:.2f})"

    else:
        return "Cannot determine asymptotic class. Please refine f(n)."

# -------------------- Decreasing Function Solver --------------------
def decreasing_solver(a, b, fn):
    fn_class = get_fn_class(fn)
    print(f"\n[INFO] Decreasing function detected. f(n) is in class: {fn_class}")

    if fn_class == "1":
        return f"Θ(n)"
    elif fn_class == "n":
        return f"Θ(n^2)"
    elif fn_class == "n^2":
        return f"Θ(n^3)"
    else:
        return "Complex decreasing function. Try solving via iteration."

# --------------- Solver for Mixed Size Divisions ----------------
def custom_solver_mixed(fn):
    print("\n[INFO] Solving for different size subproblems is custom.")
    print("[WARN] Approximation required. Final result may not be tight bound.")
    return f"T(n) = Θ(n log n) [Approximation]"

# ------------------------- Main Flow ----------------------------
def main():
    print("\n=== Recurrence Solver ===")
    print("Choose recurrence type:")
    print("1. Dividing Function")
    print("2. Decreasing Function")
    choice = input("Enter choice (1 or 2): ")

    if choice == '1':
        print("\nSelect division type:")
        print("1. T(n) = aT(n/b) + f(n)")
        print("2. T(n) = T(n/b) + T(n/b') + f(n)")
        div_type = input("Enter type (1 or 2): ")

        fn = input("Enter f(n): ")

        if div_type == '1':
            a = int(input("Enter a: "))
            b = int(input("Enter b: "))
            result = master_theorem(a, b, fn)
            print(f"\n[Result] T(n) = {result}")
        elif div_type == '2':
            result = custom_solver_mixed(fn)
            print(f"\n[Result] {result}")
        else:
            print("Invalid division type")

    elif choice == '2':
        a = int(input("Enter a: "))
        b = int(input("Enter b (subtraction amount): "))
        fn = input("Enter f(n): ")
        result = decreasing_solver(a, b, fn)
        print(f"\n[Result] T(n) = {result}")

    else:
        print("Invalid input")

# ----------------------- Execute Solver -------------------------
if __name__ == "__main__":
    main()