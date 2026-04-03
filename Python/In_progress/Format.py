def a():
    m = input()
    n = input()
    u = input()
    c = input()

    g = []

    mp = m.split(" ")
    np = n.split(" ")
    up = u.split(" ")
    uc = c.split(" ")

    for x in range(len(mp)):
        g.append(f"{{{mp[x]}")
        g.append(np[x])
        g.append(up[x])
        g.append(f"{uc[x]}}}")

    print(g)

def b():
    a = input()
    a = a.replace(",,", ",")
    print(a)

def c():
    a = []

    for x in range(0, 90):
        b = input()
        if b != "":
            a.append(b)

    print(a)

def da():
    d = 0
    a = []
    b = []

    for x in range(0 , 90):
        if x%3 == 0 and x != 0:
            for i in range(len(b)):
                d += int(b[i])
            d = round(d/3)
            a.append(d)
            d = 0
            b = []
        b.append(input())
    print(a)

b()


