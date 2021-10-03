
const getExerciseToDo = async () => {

    try {

        let response = await fetch("https://localhost:9000/v1/Routine/ExerciseToDo");

        return await response.json();

    } catch (e) {
        throw e;
    }
    
}

const showExerciseToDo = (exercise) => {
    try {

        if (!exercise.succeeded) {
            Swal.fire({ background: 'black', position: 'top-center',icon: `error`,title: `Error!`,text: `${exercise.title}`,});
            return;
        }

        const exerciseContainer = document.getElementById("exerciseToDo");

        if (exercise.content === null) {

            Swal.fire({ background: 'black', position: 'top-center', icon: `success`, title: `Done`, text: `You are done for today!`, });

            const card = document.createElement("div");
            card.className = "card bg-dark border border-light";
            card.style = "width: 28rem";

            const image = document.createElement("img");
            image.className = "card-img-top";
            image.setAttribute("src", "/Images/HappyFace.png");

            const bodyCard = document.createElement("div");
            bodyCard.className = "card-body";

            const hr = document.createElement("hr");
            hr.className = "bg-white";

            const text = document.createElement("p");
            text.className = "text-white text-center fs-4";
            text.textContent = "Go and get rest!";

            bodyCard.appendChild(text);
            bodyCard.appendChild(hr);

            card.appendChild(bodyCard);
            card.appendChild(image);

            exerciseContainer.appendChild(card);

            return;
        }

        document.getElementById("ExerciseId").value = exercise.content.id;
  
        const card = document.createElement("div");
        card.className = "card bg-dark border border-light";
        card.style = "width: 35rem";

        const image = document.createElement("img");
        image.className = "card-img-top";
        image.setAttribute("src", exercise.content.imageUrl);

        const headerCard = document.createElement("div");
        headerCard.className = "card-body";

        const hr = document.createElement("hr");
        hr.className = "bg-white";

        const text = document.createElement("p");
        text.className = "text-white text-center fs-4";
        text.textContent = exercise.content.name;

        const cardBody = document.createElement("div");
        cardBody.className = "card-body";

        const setsLeft = document.createElement("p");
        setsLeft.className = "text-white text-center fs-2";
        setsLeft.textContent = `Sets left: ${exercise.content.setsLeft}`;

        headerCard.appendChild(text);
        headerCard.appendChild(hr);

        cardBody.appendChild(setsLeft);

        card.appendChild(headerCard);
        card.appendChild(image);
        card.appendChild(cardBody);
 
        exerciseContainer.appendChild(card);

        return;


    } catch (e) {
        throw e;
    }
}

const removeExerciseDoneForm = (exercise) => {
    const result = document.getElementById("exerciseCard");

    if (result !== null || exercise.content !== null) return;

    const form = document.getElementById("exerciseDoneForm");

    while (form.firstChild) {
        form.removeChild(form.firstChild);
    }
}

const validateData = () => {
    let poundsLifted = document.getElementById("PoundsLifted").value;
    let repetitions = document.getElementById("Repetitions").value;
    let exerciseId = document.getElementById("ExerciseId").value;

    if (poundsLifted == 0 || poundsLifted == "") return false;
    if (repetitions == 0 || repetitions == "") return false;
    if (exerciseId == "") return false;

    return true;
}

const sendSetDone = async () => {

    try {

        let poundsLifted = document.getElementById("PoundsLifted").value;
        let repetitions = document.getElementById("Repetitions").value;
        let exerciseId = document.getElementById("ExerciseId").value;

        const data = {
            exerciseId: exerciseId,
            poundsLifted: poundsLifted,
            repetitions: repetitions
        }

        const settings = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        };

        const response = await fetch("https://localhost:9000/v1/Routine/ExerciseDone", settings);

        return await response.json();

    } catch (e) {
        throw e;
    }

}

window.addEventListener("load", async () => {


    try {
        document.getElementById("spinner").className = "spinner-border centered text-white";

        let exerciseToDo = await getExerciseToDo();

        showExerciseToDo(exerciseToDo);

        removeExerciseDoneForm(exerciseToDo);

        document.getElementById("spinner").className = "spinner-border centered text-white d-none";
    } catch (e) {
        Swal.fire({ position: 'top-center', icon: 'error', title: 'Error', text: `Could not register set done due to ${e.message}`, });
    }
        
});

document.getElementById("btnSetDone").addEventListener("click", async () => {

    try {

        document.getElementById("spinner").className = "spinner-border centered text-white";

        let validData = validateData();

        if (validData === false) throw new Error("The pounds lifted and repetitions must not be emtpy!");

        let response = await sendSetDone();
            
        if (!response.succeeded) throw new Error(response.title);
 
        location.reload();

        document.getElementById("spinner").className = "spinner-border centered text-white d-none";

    } catch (e) {
        Swal.fire({ position: 'top-center', icon: 'error', title: 'Error', text: `Could not register set done due to ${e.message}`, });
    }

});