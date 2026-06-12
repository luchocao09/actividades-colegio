const evento_musical=document.getElementById("evento_musical")
evento_musical.addEventListener("change",function(){
    if (cant_invitados>=300)
        evento_musical.style.visibility="visible";
    else
        evento_musical.style.visibility="hidden";

})
const Calcular_boton = document.getElementById("Calcular")
Calcular_boton.addEventLister("click", function () {

let errores=""
let valor_reserva=0
const resultados=document.getElementById("resultados")
const cant_invitados=document.getElementById("cant_invitados")
const nombre=document.getElementById("nombre")
const size_salon=document.getElementById("size_salon")


resultados.innerHTML=valor_reserva



















})