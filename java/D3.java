import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;

class D3 {
    public static void main(String[] args) throws Exception {
        System.out.println(p1());
        System.out.println(p2());
    }

    private static int TreeSlope(ArrayList<char[]> map, int dr, int dc) {
        int trees = 0, r = 0, c = 0, w = map.get(0).length, h = map.size();

        while (r < h) {
            if (map.get(r)[c % w] == '#')
                trees++;
            r += dr; c += dc;
        }

        return trees;
    }

    private static int p1() throws Exception {
        return TreeSlope(parseInput(), 1, 3);
    }

    private static long p2() throws Exception {
        var map = parseInput();
        return (long)TreeSlope(map, 1, 1)
             * (long)TreeSlope(map, 1, 3)
             * (long)TreeSlope(map, 1, 5)
             * (long)TreeSlope(map, 1, 7)
             * (long)TreeSlope(map, 2, 1);
    }

    private static ArrayList<char[]> parseInput() throws Exception {
        ArrayList<char[]> map = new ArrayList<char[]>();
        for (var line : Files.readAllLines(Path.of("../inputs/D3.input")))
        {
            map.add(line.toCharArray());
        }
        return map;
    }
}