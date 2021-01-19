
def parseInput():
    file = open("../inputs/D9.input", "r")
    return list(map(int, file.read().splitlines()))

def findWeakness(numbers):
    for i in range(25, len(numbers)):
        found = False
        for j in range(i - 25, i):
            for k in range(j + 1, i):
                if (numbers[j] + numbers[k] == numbers[i]):
                    found = True
                    break
            if found: break
        if not found: return numbers[i]

def p1():
    return findWeakness(parseInput())

def p2():
    numbers = parseInput()
    weakness = findWeakness(numbers)
    L = 0
    R = 1
    sum = numbers[0] + numbers[1]
    while sum != weakness:
        if sum > weakness:
            sum -= numbers[L]
            L += 1
        else:
            R += 1
            sum += numbers[R]

    return max(numbers[L:R+1]) + min(numbers[L:R+1])

print(p1())
print(p2())
