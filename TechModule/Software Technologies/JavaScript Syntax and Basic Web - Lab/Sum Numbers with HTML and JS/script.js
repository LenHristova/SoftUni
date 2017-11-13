function sumNumbers() {
    let num1 = Number(document.getElementById('num1').value);
    let num2 = Number(document.getElementById('num2').value);
    let sum = num1 + num2;

    if (Number.isNaN(sum))
    {
        document.getElementById('result').innerHTML = '<span class="invalid">Invalid input!</span>';
    }else {
        document.getElementById('result').innerHTML = `<span class="valid">${sum}</span>`;
    }
}