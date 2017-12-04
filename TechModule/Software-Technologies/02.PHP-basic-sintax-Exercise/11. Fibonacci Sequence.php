<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>First Steps Into PHP</title>

</head>
<body>
    <form>
        N: <input type="text" name="num" />
        <input type="submit" />
    </form>
    <?php
    if (isset($_GET['num'])){
        $num = intval($_GET['num']);

        $fib1 = 1;
        $fib2 = 1;
        for ($i = 0; $i < $num; $i++){
            echo $fib1 . " ";
            $newFib2 = $fib1 + $fib2;
            $fib1 = $fib2;
            $fib2 = $newFib2;
        }
    }
    ?>
</body>
</html>