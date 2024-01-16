
function validateForm() {
    var name = document.getElementById('InputName').value;
    if (name.trim() === '') {
        alert('Name is required');
        return false;
    }


    return true;
}