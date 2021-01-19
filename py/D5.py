
from os import replace


def parseInput():
    file = open("../inputs/D5.input", "r")
    seats = []
    contents = file.read()
    for line in contents.splitlines():
        seats.append(line)
    return seats


def binary(s: str):
    bin = (s.replace("F", "0")
           .replace("B", "1")
           .replace("L", "0")
           .replace("R", "1"))
    return int(bin, 2)

def seatId(seat: str):
    row = binary(seat[0:7])
    col = binary(seat[7:])
    return row * 8 + col

def p1():
    return max(map(seatId, parseInput()))

def p2():
    seatIds = sorted(map(seatId, parseInput()))
    for i in range(len(seatIds) - 1):
        if seatIds[i] + 1 != seatIds[i + 1]:
            return seatIds[i] + 1


print(p1())
print(p2())
