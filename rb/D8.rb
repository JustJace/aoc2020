require "set"

$JMP = "jmp"
$NOP = "nop"
$ACC = "acc"

Instruction = Struct.new(:op, :arg)

def parseInput()
  program = []
  File.open("../inputs/D8.input", "r") do |f|
    for line in f.each_line
      line.chomp =~ /(.{3}) (.+)/
      program.append(Instruction.new($1, Integer($2)))
    end
  end
  return program
end

def run(program)
  pptr = 0
  acc = 0
  seen = Set[]
  while true
    return acc, false if seen.include? pptr
    return acc, true if pptr >= program.length
    seen.add pptr
    case program[pptr].op
    when $NOP
      pptr += 1
    when $JMP
      pptr += program[pptr].arg
      next
    when $ACC
      acc += program[pptr].arg
      pptr += 1
    end
  end
end

def p1()
  return (run parseInput)[0]
end

def p2()
  program = parseInput()
  for i in 0...program.length
    modified = parseInput()
    case modified[i].op
    when $ACC 
      next
    when $JMP 
      modified[i].op = $NOP
    when $NOP 
      modified[i].op = $JMP
    end
    acc, halts = run (modified)
    return acc if halts
  end
end

puts p1()
puts p2()