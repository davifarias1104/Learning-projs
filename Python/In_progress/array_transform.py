"""
array_transform.py

Find a single formula f(x) that maps elements of array A to array B.
Tries (in order):
 - affine mapping y = a*x + b (exact)
 - minimal-degree polynomial interpolation (exact, degree <= n-1)
 - (optional) least-squares polynomial fit if exact mapping not possible

Usage examples at the bottom show how to call `find_formula` and `apply_formula`.
"""
from __future__ import annotations
from fractions import Fraction
from typing import List, Tuple, Optional


def is_affine(xs: List[float], ys: List[float], tol: float = 1e-9) -> Optional[Tuple[float, float]]:
    if len(xs) != len(ys) or len(xs) < 1:
        return None

    # If all xs equal, mapping must be constant
    if all(abs(x - xs[0]) < tol for x in xs):
        if all(abs(y - ys[0]) < tol for y in ys):
            return 0.0, ys[0]  # constant function
        return None

    # find two distinct points
    i, j = None, None
    for idx in range(1, len(xs)):
        if abs(xs[idx] - xs[0]) > tol:
            i, j = 0, idx
            break

    if i is None:
        return None

    a = (ys[j] - ys[i]) / (xs[j] - xs[i])
    b = ys[i] - a * xs[i]

    # verify
    for x, y in zip(xs, ys):
        if abs(a * x + b - y) > tol:
            return None
    return a, b


def solve_vandermonde_fraction(xs: List[float], ys: List[float], deg: int) -> Optional[List[Fraction]]:
    n = deg + 1
    # build matrix A and vector y with Fractions
    A = [[Fraction(1)] * n for _ in range(n)]
    for i in range(n):
        xi = Fraction(xs[i])
        A[i][0] = Fraction(1)
        for j in range(1, n):
            A[i][j] = A[i][j - 1] * xi
    b = [Fraction(ys[i]) for i in range(n)]

    # Gaussian elimination
    # convert to augmented matrix
    M = [row[:] + [bval] for row, bval in zip(A, b)]
    N = len(M)

    for k in range(N):
        # pivot
        pivot = None
        for i in range(k, N):
            if M[i][k] != 0:
                pivot = i
                break
        if pivot is None:
            return None
        if pivot != k:
            M[k], M[pivot] = M[pivot], M[k]

        # normalize row
        pivot_val = M[k][k]
        M[k] = [val / pivot_val for val in M[k]]

        # eliminate below
        for i in range(N):
            if i == k:
                continue
            factor = M[i][k]
            if factor != 0:
                M[i] = [vi - factor * vk for vi, vk in zip(M[i], M[k])]

    coeffs = [row[-1] for row in M]
    return coeffs


def find_minimal_polynomial(xs: List[float], ys: List[float]) -> Optional[List[Fraction]]:
    n = len(xs)
    if n == 0:
        return None

    # try degrees 0..n-1 using first (deg+1) distinct samples
    for deg in range(0, n):
        # choose deg+1 samples (first ones)
        if deg + 1 > n:
            break
        coeffs = solve_vandermonde_fraction(xs, ys, deg)
        if coeffs is None:
            continue
        # verify full data
        ok = True
        for x, y in zip(xs, ys):
            val = Fraction(0)
            powx = Fraction(1)
            for c in coeffs:
                val += c * powx
                powx *= Fraction(x)
            if val != Fraction(y):
                ok = False
                break
        if ok:
            return coeffs
    return None


def format_fraction(fr: Fraction) -> str:
    if fr.denominator == 1:
        return str(fr.numerator)
    return f"({fr.numerator}/{fr.denominator})"


def format_polynomial(coeffs: List[Fraction]) -> str:
    terms = []
    for i, c in enumerate(coeffs):
        if c == 0:
            continue
        coef = format_fraction(c)
        if i == 0:
            terms.append(f"{coef}")
        elif i == 1:
            terms.append(f"{coef}*x")
        else:
            terms.append(f"{coef}*x**{i}")
    if not terms:
        return "0"
    return " + ".join(reversed(terms))


def find_formula(xs: List[float], ys: List[float]) -> str:
    # try affine
    aff = is_affine(xs, ys)
    if aff is not None:
        a, b = aff
        return f"y = {a}*x + {b}  (affine)"

    # try minimal polynomial
    coeffs = find_minimal_polynomial(xs, ys)
    if coeffs is not None:
        return "y = " + format_polynomial(coeffs) + "  (polynomial exact)"

    # fallback: least-squares linear (nice attempt)
    try:
        import numpy as np
        deg = min(len(xs) - 1, 3)  # try small degree
        p = np.polyfit(xs, ys, deg)
        terms = []
        for i, c in enumerate(p[::-1]):
            if abs(c) < 1e-12:
                continue
            power = i
            if power == 0:
                terms.append(f"{c:.6g}")
            elif power == 1:
                terms.append(f"{c:.6g}*x")
            else:
                terms.append(f"{c:.6g}*x**{power}")
        return "y = " + " + ".join(reversed(terms)) + "  (least-squares approx)"
    except Exception:
        return "No simple exact formula found; consider adding more sample pairs or allow approximation."


def apply_polynomial_fraction(coeffs: List[Fraction], x: float) -> float:
    res = Fraction(0)
    powx = Fraction(1)
    for c in coeffs:
        res += c * powx
        powx *= Fraction(x)
    return float(res)


if __name__ == "__main__":
    # Example usage
    A = []
    # target: scale and shift (example)
    B = []

    print("A:", A)
    print("B:", B)
    print("Finding formula...")
    print(find_formula(A, B))

    # Another example with an exact quadratic
    A2 = [0, 1, 2]
    B2 = [1, 4, 9]  # y = x**2 + 1
    print('\nA2:', A2)
    print('B2:', B2)
    print('Formula:', find_formula(A2, B2))
    coeffs = find_minimal_polynomial(A2, B2)
    if coeffs:
        print('Coeffs (low->high):', coeffs)
        print('Apply to 3 ->', apply_polynomial_fraction(coeffs, 3))
