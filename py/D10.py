def parseInput():
    file = open("../inputs/D10.input", "r")
    jolts = []
    contents = file.read()
    for line in contents.splitlines():
        jolts.append(int(line))
    return jolts
    
def p1():
    jolts = parseInput()
    jolts.append(0)
    jolts.append(max(jolts) + 3)
    jolts.sort()

    ones = 0
    threes = 0

    for i in range(len(jolts) - 1):
        diff = jolts[i + 1] - jolts[i]
        if diff == 1: ones += 1
        elif diff == 3: threes += 1

    return ones * threes

def p2():
    jolts = parseInput()
    jolts.append(0)
    jolts.append(max(jolts) + 3)
    jolts.sort()
    dp = {}
    dp[len(jolts)-1] = 0
    dp[len(jolts)-2] = 1

    for i in reversed(range(len(jolts) - 2)):
        dp[i] = 0

        if jolts[i + 1] - jolts[i] <= 3:
            dp[i] += dp[i + 1]
        
        if jolts[i + 2] - jolts[i] <= 3:
            dp[i] += dp[i + 2]
        
        if i + 3 < len(jolts) and jolts[i + 3] - jolts[i] <= 3:
            dp[i] += dp[i + 3]

    return dp[0]

print(p1())
print(p2())