const addCategoriesToCombo = categories => {

    const categoriesCombo = document.getElementById("categories");

    for (let i = 0; i < categories.length; i++) {

        const option = document.createElement("option");
        option.setAttribute("value", categories[i].categoryId);
        option.textContent = categories[i].categoryName;

        categoriesCombo.appendChild(option);
    }

};

const showError = errorMessage => {
    Swal.fire({
        background:'black',
        position: 'top-center',
        icon: 'error',
        title: 'Error',
        text: `We could not load the categories because of ${errorMessage}`,
        customClass: {
            'title': 'text-white',
            'text':'text-white'
        }
    });
}

window.addEventListener("load", async () => {

    try {
        var response = await fetch("https://localhost:9000/v1/Categories");

        var result = await response.json();

        (result.succeeded) ? addCategoriesToCombo(result.content) : showError(result.title);
    } catch (e) {
        showError(e.errorMessage);
    }

});

