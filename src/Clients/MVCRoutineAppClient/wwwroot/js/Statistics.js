const getCategories = async () =>{
    try {
        
        const response = await fetch("https://localhost:9000/v1/Categories");
        
        return await response.json();
        
    }catch(e){
        throw e;
    }
}

const getExercises = async() =>{
    try {

        const categoryId = document.getElementById("categories").options[document.getElementById("categories").selectedIndex].value;
        
        const response = await fetch(`https://localhost:9000/v1/Exercise/Category/NameAndId/${categoryId}`);
        
        return await response.json();
        
    }catch(e){
        throw e;
    }
}

const addCategoriesToSelect = (response) =>{
    try {
        
        if(!response.succeeded) throw new Error("The categories could not be achieved!");
        
        const categories = document.getElementById("categories");
        
        for(let i = 0; i < response.content.length; i++){
            let category = response.content[i];
            
            let option = document.createElement("option");
            option.className = "text-white";
            option.setAttribute("value", category.categoryId);
            option.textContent = category.categoryName;
            
            categories.appendChild(option);
        }
        
    }catch(e){
        throw e;
    }
}

const addExercisesToSelect = (response) =>{
    try {

        if(!response.succeeded && response.statusCode !==404) throw new Error("Error while getting the exercises!");

        let exercises = document.getElementById("exercises");

        while (exercises.firstChild) {
            exercises.removeChild(exercises.firstChild);
        }

        if (response.statusCode === 404) {
            return;
        } 
        
        for(let i = 0; i < response.content.length; i++){
            let exercise = response.content[i];

            let option = document.createElement("option");
            option.className = "text-white";
            option.setAttribute("value", exercise.exerciseId);
            option.textContent = exercise.exerciseName;

            exercises.appendChild(option);
        }

    }catch(e){
        throw e;
    }
}

const addFiltersForm = () =>{
    
    document.getElementById("instructions").className = "text-white";
    document.getElementById("months").className = "bg-dark text-white";
    document.getElementById("years").className = "bg-dark text-white";
    document.getElementById("limit").className = "bg-white";

}

const removeFiltersForm = () =>{

    document.getElementById("instructions").className ="text-white d-none";
    document.getElementById("months").className ="bg-dark text-white d-none";
    document.getElementById("years").className ="bg-dark text-white d-none";
    document.getElementById("limit").className = "bg-white d-none";

}

const getStatistics = async () =>{
    try {

        const exerciseIndex = document.getElementById("exercises").selectedIndex;
        
        if(exerciseIndex === -1) return null;
        
        const exerciseId = document.getElementById("exercises").options[exerciseIndex].value;

        const monthElement = document.getElementById("months");
        const yearElement = document.getElementById("years");

        let year = 0;
        let month = 0;

        const allStatistics = document.getElementById("flexCheckChecked").checked;

        if (!allStatistics) {
            month = monthElement.options[monthElement.selectedIndex].value;
            year = yearElement.options[yearElement.selectedIndex].value;
        }

        const response = await fetch(`https://localhost:9000/v1/Statistics/${exerciseId}/${month}/${year}`);
        return await response.json();
        
    }catch(e){
        throw e;
    }
}

const getColor = (number) => {
    switch (number) {
        case 1:
            return { background: 'rgba(255, 99, 132, 0.2)', border: 'rgba(255, 99, 132, 1)'};
        case 2:
            return { background: 'rgba(54, 162, 235, 0.2)', border: 'rgba(54, 162, 235, 1)'};
        case 3:
            return { background: 'rgba(255, 206, 86, 0.2)', border: 'rgba(255, 206, 86, 1)'};
        case 4:
            return { background: 'rgba(75, 192, 192, 0.2)', border: 'rgba(75, 192, 192, 1)'};
        case 5:
            return { background: 'rgba(153, 102, 255, 0.2)', border: 'rgba(153, 102, 255, 1)'};
        default:
            return { background: 'rgba(255, 159, 64, 0.2)', border: 'rgba(255, 159, 64, 1)'};
    }
}

const prepareStatisticsForChart = (statistics) => {
    const dates = [];
    const values = [];
    const backgroundColors = [];
    const borderColors = [];

    const type = document.getElementById("type").options[document.getElementById("type").selectedIndex].value;

    for (var i = 0; i < statistics.length; i++) {

        dates.push(statistics[i].date);
        (type === "Weight") ? values.push(statistics[i].weight) : values.push(statistics[i].repetitions);
        colors = getColor(Math.floor(Math.random() * (7 - 1)) + 1);
        backgroundColors.push(colors.background);
        borderColors.push(colors.border);
    }

    return { dates: dates, values: values, backgroundColors: backgroundColors, borderColors: borderColors };
}

const addStatisticsToView = (response) =>{
    try {

        const statisticsContainer = document.getElementById("statistics");

        while (statisticsContainer.firstChild) {
            statisticsContainer.removeChild(statisticsContainer.firstChild);
        }


        if ((response === null) || (!response.succeeded && response.statusCode === 404)) {

            const chart = document.getElementById("myChart");

            if(chart !== null)chart.remove();

            const card = document.createElement("div");
            card.className = "card bg-dark border border-light";
            card.style = "width: 28rem";

            const image = document.createElement("img");
            image.className = "card-img-top";
            image.setAttribute("src", "/Images/SadFace.png");

            const bodyCard = document.createElement("div");
            bodyCard.className = "card-body";

            const hr = document.createElement("hr");
            hr.className = "bg-white";

            const text = document.createElement("p");
            text.className = "text-white text-center fs-4";
            text.textContent = "There are no statistics for the specific exercise";

            bodyCard.appendChild(text);
            bodyCard.appendChild(hr);

            card.appendChild(bodyCard);
            card.appendChild(image);

            statisticsContainer.appendChild(card);

            return;
        }

        if (!response.succeeded && response.statusCode !== 404) throw new Error("Error while getting the statistics for the exercise");

        const chart = document.createElement("canvas");
        chart.setAttribute("id", "myChart");
        chart.setAttribute("width", "500");
        chart.setAttribute("height", "400");
        

        statisticsContainer.appendChild(chart);

        const { dates, values, backgroundColors, borderColors } = prepareStatisticsForChart(response.content);

        var ctx = document.getElementById('myChart').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: dates,
                datasets: [{
                    label: document.getElementById("type").options[document.getElementById("type").selectedIndex].value,
                    data: values,
                    backgroundColor: backgroundColors,
                    borderColor: borderColors,
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true
                    }
                }
            }
        });
        
        
    }catch(e){
        throw e;
    }
}

window.addEventListener("load", async () =>{
    try {

        document.getElementById("spinner").className = "spinner-border centered text-white";

        const categoriesResult = await getCategories();
        addCategoriesToSelect(categoriesResult);
        
        const exercisesResult = await getExercises();
        addExercisesToSelect(exercisesResult);
        
        const statisticsResponse = await getStatistics();
        addStatisticsToView(statisticsResponse);
        
        document.getElementById("spinner").className = "spinner-border centered text-white d-none";

    }catch (e){
        Swal.fire({background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`,});
    }
})

document.getElementById("flexCheckChecked").addEventListener("change", async() =>{
   
    try {

        document.getElementById("spinner").className = "spinner-border centered text-white";

        const value = document.getElementById("flexCheckChecked").checked;

        (value)?removeFiltersForm():addFiltersForm();

        const statistics = await getStatistics();

        addStatisticsToView(statistics);

        document.getElementById("spinner").className = "spinner-border centered text-white d-none";

    }catch(e){
        Swal.fire({background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`,});
    }
   
});

document.getElementById("categories").addEventListener("change", async () => {
    try {

        document.getElementById("spinner").className = "spinner-border centered text-white";

        const exercisesResult = await getExercises();
        addExercisesToSelect(exercisesResult);
        const statistics = await getStatistics();
        addStatisticsToView(statistics);

        document.getElementById("spinner").className = "spinner-border centered text-white d-none";
    }catch(e){
        Swal.fire({background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`,});
    }
});

document.getElementById("exercises").addEventListener("change", async () => {
    try {
        document.getElementById("spinner").className = "spinner-border centered text-white";
        const statistics = await getStatistics();
        addStatisticsToView(statistics);
        document.getElementById("spinner").className = "spinner-border centered text-white d-none";
    }catch(e){
        Swal.fire({background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`,});
    }
});

document.getElementById("type").addEventListener("change", async () => {
    try {
        document.getElementById("spinner").className = "spinner-border centered text-white";
        const statistics = await getStatistics();
        addStatisticsToView(statistics);
        document.getElementById("spinner").className = "spinner-border centered text-white d-none";

    } catch (e) {
        Swal.fire({ background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`, });
    }
});

document.getElementById("months").addEventListener("change", async () => {
    try {
        document.getElementById("spinner").className = "spinner-border centered text-white";
        const statistics = await getStatistics();
        addStatisticsToView(statistics);
        document.getElementById("spinner").className = "spinner-border centered text-white d-none";
    } catch (e) {
        Swal.fire({ background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`, });
    }
});

document.getElementById("years").addEventListener("change", async () => {
    try {
        document.getElementById("spinner").className = "spinner-border centered text-white";
        const statistics = await getStatistics();
        addStatisticsToView(statistics);
        document.getElementById("spinner").className = "spinner-border centered text-white d-none";
    } catch (e) {
        Swal.fire({ background: 'black', position: 'top-center', icon: `error`, title: `Error`, text: `${e.message}`, });
    }
});
