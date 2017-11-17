function multiplyValuesForKey(arr) {

    let searchedKey = arr.pop();

    let dict = {};
    for (let str of arr) {
        let keyValue = str.split(' ');
        let key = keyValue[0];
        let value = keyValue[1];
        if (!(key in dict)) {
            dict[key] = [value];
        }else {
            dict[key].push(value);
        }
    }
    console.log(dict[searchedKey] === undefined ? 'None' : dict[searchedKey].join('\n'));
}

multiplyValuesForKey(['key value', 'key eulav', 'test tset', 'key']);
//multiplyValuesForKey(['3 test', '3 test1', '4 test2', '4 test3', '4 test5', '4']);
//multiplyValuesForKey(['3 bla', '3 alb', '2']);