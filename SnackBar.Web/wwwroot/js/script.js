
function sendType(product){
    const xhr = new XMLHttpRequest()
    xhr.open('POST', '/Home/ShowType')
    xhr.setRequestHeader('Content-Type', 'application/json', true)

    xhr.onload = () => {
        if (xhr.status === 200) {
            const status = JSON.parse(xhr.responseText).success
            console.log(status)
        }
    }

    console.log(product)

    xhr.send(JSON.stringify({ itemType: product }))
};