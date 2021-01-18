def parseInput():
    file = open("../inputs/D1.input", "r")
    contents = file.read()
    return list(map(int, contents.splitlines()))

def p1():
    numbers = sorted(parseInput())
    L = 0
    R = len(numbers) - 1
    while (numbers[L] + numbers[R] != 2020):
        if (numbers[L] + numbers[R] < 2020):
            L += 1
        else:
            R -= 1
    return numbers[L] * numbers[R]

def p2():
    numbers = sorted(parseInput())
    for i in range(len(numbers)):
        L = i + 1
        R = len(numbers) - 1
        remainder = 2020 - numbers[i]
        while (L < R):
            if (numbers[L] + numbers[R] == remainder):
                return numbers[L] * numbers[R] * numbers[i]
            elif (numbers[L] + numbers[R] < remainder):
                L += 1
            else:
                R -= 1

print(p1())
print(p2())