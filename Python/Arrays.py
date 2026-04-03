# Arrays.py
import os

os.system('cls' if os.name == 'nt' else 'clear')
Col = int(input("Col: "))
Row = int(input("Row: "))
Sum = [0, 1, 0, 0]

A = [[input(f"{n, i}:") for i in range(Row)] for n in range(Col)]

for i in range(len(A)):
    for n in range(len(A)):
        Sum[2] += int(A[i][n])
    Sum[0] += int(A[i][0])
    Sum[1] *= int(A[0][i])
    Sum[3] += int(A[i][i])
print(f"\n{Sum}")