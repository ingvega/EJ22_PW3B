var animaciones=[];
/*var alumno={
    'nombre':'Juan Perez',
    'carrera':Sistemas,
    'semestre':1,
    materias:[
        {
            nombre:'matematicas',
            calif:90
        },
        {
            nombre:'español',
            calif:100
        }
    ]    
};

alumno.nombre*/

//DOM (Document Object Model)
//Se ejecuta cuando ya está listo el árbol que representa
//todo el desglose de elementos del documento
//document.addEventListener("DOMContentLoaded",function(){
    //Configurar las animaciones
//});

//Se ejecuta cuando ya está listo el DOM y además todos 
//los recursos (imagenes, videos, estilos, librerías) 
//locales y externos se han cargado
window.addEventListener("load",function(){
    //Configurar las animaciones
    var letras=document.getElementsByClassName('letra');

    /*letras[0].animate(
        [{keyframes}],
        {configuración de la animación}
    );*/
    var retardo=500;
    
    for(var i=0;i<letras.length;i++){
        animaciones[i]=letras[i].animate(
            [
                //0% o from
                { transform:'rotateY(180deg)', backgroundColor:'white' },
                //100% o to
                { transform:'rotateY(0deg)' , backgroundColor:'#ddd' }
            ],
            {
                duration:2000,
                delay:retardo,
                fill: 'forwards'
            }
        );
        retardo+=500;
    }

    
});

function acelerar(){
    for(var i=0;i<animaciones.length;i++){
        animaciones[i].playbackRate++;
    }
}

function desacelerar(){
    for(var i=0;i<animaciones.length;i++){
        animaciones[i].playbackRate--;
    }
}

function pausar(){
    for(var i=0;i<animaciones.length;i++){
        animaciones[i].playbackRate=0;
    }
}

/*
var boton=document.getElementById("btnAccion");
boton.addEventListener("change",function(){
    //Acciones a ejecutar después del click
});
*/


