<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>First Steps Into PHP</title>

</head>
<body>
<?php
if (isset($_GET['num'])) {
    $res = intval($_GET['num']) * 2;
}
?>
<form>
    N: <input type="text" name="num" value="<?=$res ?>"/>
    <input type="submit"/>
</form>
</body>
</html>

