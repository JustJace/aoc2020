import java.nio.file.Files;
import java.nio.file.Path;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.stream.Collectors;

class D5 {
    public static void main(String[] args) throws Exception {
        System.out.println(p1());
        System.out.println(p2());
    }

    private static int binary(String s) {
        s = s.replace("L", "0")
         .replace("R", "1")
         .replace("F", "0")
         .replace("B", "1");
        
        return Integer.parseInt(s, 2);
    }

    private static int seatId(String seat) {
        var row = binary(seat.substring(0,7));
        var col = binary(seat.substring(7));
        return row * 8 + col;
    }

    private static int p1() throws Exception {
        var max = Integer.MIN_VALUE;
        for (var seat : parseInput()) {
            var id = seatId(seat);
            if (id > max) max = id;
        }
        return max;
    }

    private static int p2() throws Exception {
        var ids = parseInput().stream().map(s -> seatId(s)).collect(Collectors.toList());
        Collections.sort(ids);

        for (var i = 0; i < ids.size(); i++) {
            if (ids.get(i) + 1 != ids.get(i + 1)) 
                return ids.get(i) + 1;
        }

        return -1;
    }

    private static ArrayList<String> parseInput() throws Exception {
        ArrayList<String> seats = new ArrayList<String>();
        for (var line : Files.readAllLines(Path.of("../inputs/D5.input")))
        {
            seats.add(line);
        }
        return seats;
    }
}