
const new_admin_key = () => {

    const xhr = new XMLHttpRequest()

    xhr.open('POST', '/Admin/CreateAdminKey')

    xhr.setRequestHeader('Content-Type', 'application/json')

    xhr.onload = () => {
        if (xhr.status == 200) {
            const response = JSON.parse(xhr.responseText)
            document.getElementById('admin-key-span').textContent = response.key;
        }
    }

    xhr.send();
}