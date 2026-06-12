
const Calcular_boton = document.getElementById("Calcular")
Calcular_boton.addEventListener("click", function () {

    const div_reserva = document.getElementById("resultados")
    const cant_invitados = document.getElementById("cant_invitados")
    const nombre = document.getElementById("nombre")
    const valor_salon = document.getElementById("size_salon").value
    const evento_musical = document.getElementById("evento_musical")
    let errores = ""
    let valor_reserva=valor_salon
    if (!Number.isNaN(Number(nombre.value)))
        errores="El nombre no puede tener numeros.<br>";
    if (evento_musical.checked)
        valor_reserva=valor_reserva*1.3;
    

    if (errores=="")
    div_reserva.innerHTML = "El valor de la reserva es: "+valor_reserva+"<br>Y el valor de la senia es: "+valor_reserva*0.35;
    else
    div_reserva.innerHTML = errores;
        

})
const div_evento = document.getElementById("div_evento")
cant_invitados.addEventListener("change", function () {

    if (Number(cant_invitados.value) >= 300)
        div_evento.style.visibility = "visible";
    else
        div_evento.style.visibility = "hidden";


})















