import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.Collections;

class D1 {
    public static void main(String[] args) throws Exception {
        System.out.println(p1());
        System.out.println(p2());
    }

    public static int p1() throws Exception {
        var nums = parseInput();
        Collections.sort(nums);
        var L = 0;
        var R = nums.size() - 1;
        while (nums.get(L) + nums.get(R) != 2020) {
            if (nums.get(L) + nums.get(R) < 2020)
                L++;
            else
                R--;
        }
        return nums.get(L) * nums.get(R);
    }

    public static int p2() throws Exception {
        var nums = parseInput();
        Collections.sort(nums);
        for (var i = 0; i < nums.size(); i++) {
            var remainder = 2020 - nums.get(i);
            var L = i + 1;
            var R = nums.size() - 1;
            while (L < R) {
                var left = nums.get(L);
                var right = nums.get(R);
                if (left + right == remainder)
                    return left * right * nums.get(i);
                else if (left + right < remainder)
                    L++;
                else
                    R--;
            }
        }
        return -1;
    }

    public static ArrayList<Integer> parseInput() throws Exception {
        ArrayList<Integer> numbers = new ArrayList<Integer>();
        for (var line : Files.readAllLines(Path.of("../inputs/D1.input")))
        {
            numbers.add(Integer.parseInt(line));
        }
        return numbers;
    }
}