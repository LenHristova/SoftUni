function printLines(lines) {

    for (let line of lines){
        if(line === 'Stop'){
            break;
        }
        console.log(line);
    }
}

printLines(['3', '6', '5','4','Stop','10','12']);