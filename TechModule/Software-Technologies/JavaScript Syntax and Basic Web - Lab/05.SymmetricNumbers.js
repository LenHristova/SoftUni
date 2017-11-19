function findSymmetricNumbers([input]) {
    let n = input.split(' ')[0];
    let res = '';
    for (let i = 1; i <= n; i++) {
        if (isSymmetric(i.toString())) {
            res += i + ' ';
        }

        function isSymmetric(str) {
            let hasSymmetric = true;
            for (let i = 0; i < str.length / 2; i++) {
                if (str[i] === str[str.length - 1 - i]) {
                    continue;
                }
                return false;
            }
            return hasSymmetric;
        }
    }

    console.log(res)
}

findSymmetricNumbers(['100'])