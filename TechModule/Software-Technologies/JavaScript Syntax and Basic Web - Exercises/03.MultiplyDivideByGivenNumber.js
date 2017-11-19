function multiplyOrDivideByGivenNumber(input) {
    let n = Number(input[0]);
    let x = Number(input[1]);

    if (x >= n){
        console.log(n * x);
    }else {
        console.log(n / x);
    }
}

multiplyOrDivideByGivenNumber(['3', '2']);