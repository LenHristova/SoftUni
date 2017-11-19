function addRemoveElements(input) {
    let arr = [];

    for (let str of input) {
        let commandArgs = str.split(' ');
        let command = commandArgs[0];
        switch (command) {
            case 'add':
                let number = Number(commandArgs[1]);
                arr.push(number);
                break;
            case 'remove':
                let index = Number(commandArgs[1]);
                arr.splice(index, 1);
                break;
        }
    }

    for (let num of arr) {
        console.log(num)
    }
}

addRemoveElements(['add 3', 'add 5', 'remove 2', 'remove 0', 'add 7']);
