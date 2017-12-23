<?php
if (isset($_GET['text'])) {
    preg_match_all('/\w+/', $_GET['text'], $words);
    $words = $words[0];

    $upperWords = implode("\n", array_filter($words, function ($word) {
        return strtoupper($word) == $word;
    }));
}
?>

<form>
    <textarea rows="10" name="text"><?=$upperWords?></textarea> <br>
    <input type="submit" value="Extract">
</form>

