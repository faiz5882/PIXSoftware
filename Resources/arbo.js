// Fonction qui affiche/cache un menu
function expand(li) {
	var node = li.firstChild;
	var img = li.firstChild;
	// parcours tous les fils pour trouver l'element UL
	while ( node.nodeName != "UL" )
		node = node.nextSibling;
	// parcours tous les fils pour trouver l'element IMG
	while ( img.nodeName != "IMG" )
		img = img.nextSibling;
	// affiche le menu
	if ( node.style.display == 'none' ) {
		node.style.display = 'block';
		img.src = 'Images/arbo_minus.gif';
		img.alt = '[-]';
	}
	// cache le menu
	else {
		node.style.display = 'none';
		img.src = 'Images/arbo_plus.gif';
		img.alt = '[+]';
	}
}

// Fonction qui affiche/cache un menu
function forceExpand(li) { // not yet implemented
	var node = li.firstChild;
	var img = li.firstChild;
	// parcours tous les fils pour trouver l'element UL
	while ( node.nodeName != "UL" )
		node = node.nextSibling;
	// parcours tous les fils pour trouver l'element IMG
	while ( img.nodeName != "IMG" )
		img = img.nextSibling;
	// affiche le menu
	if ( node.style.display == 'none' ) {
		node.style.display = 'block';
		img.src = 'Images/arbo_minus.gif';
		img.alt = '[-]';
	}
}

// Fonction qui affiche/cache un menu
function forceCollapse(li) { 
	var node = li.firstChild;
	var img = li.firstChild;
	// parcours tous les fils pour trouver l'element UL
	while ( node.nodeName != "UL" )
		node = node.nextSibling;
	// parcours tous les fils pour trouver l'element IMG
	while ( img.nodeName != "IMG" )
		img = img.nextSibling;
	// affiche le menu
	if ( node.style.display != 'none' ) {
		node.style.display = 'none';
		img.src = 'Images/arbo_plus.gif';
		img.alt = '[+]';
	}
}

// Fonction qui affiche un menu ainsi que tous les menus supérieurs
function expandMultiple(id) {
	// recupere la source du clic
	a = document.getElementById(id);
	// recupere le menu a afficher
	var ul = a;
	while ( ul.nodeName != "UL" ) {
		ul = ul.nextSibling;
	}
	// affiche le menu et les menus supérieurs
	while(ul.nodeName=="UL") {
		if(ul.id != 'racine') {
			expand(ul.parentNode);
			ul = ul.parentNode.parentNode;
		}
	}
}

// Fonction qui cache tous les menus
function collapseMultiple() {
	// Recupere le menu de niveau 1
	niv_1 = document.getElementById('niv1');
	// recupere tous les menus dépliants
	tab_ul = niv_1.getElementsByTagName("UL");
	nb = tab_ul.length;
	// cache tous les menus
	for(var i=0; i<nb; i++) {
		expand(tab_ul[i].parentNode);
	}
}

// Fonction qui initialise l'arborescence
function initArbo(menu, smenu) {
	// ferme tous les menus
	collapseMultiple();
	// ouvre le smenu courant (passé en GET) si il existe
	if(document.getElementById(smenu)) {
		expandMultiple(smenu);
	}
	else {
		// sinon ouvre le menu courant (passé en GET) si il existe
		if(document.getElementById(menu)) {
			expandMultiple(menu);
		}
	}
}

// Fonction qui affiche tous les menus
function expandAll() { // not yet implemented
	// Recupere le menu de niveau 1
	niv_1 = document.getElementById('niv1');
	// recupere tous les menus dépliants
	tab_ul = niv_1.getElementsByTagName("UL");
	nb = tab_ul.length;
	// cache tous les menus
	for(var i=0; i<nb; i++) {
		forceExpand(tab_ul[i].parentNode);
	}
}

// Fonction qui cache tous les menus
function collapseAll() { // not yet implemented
	// Recupere le menu de niveau 1
	niv_1 = document.getElementById('niv1');
	// recupere tous les menus dépliants
	tab_ul = niv_1.getElementsByTagName("UL");
	nb = tab_ul.length;
	// cache tous les menus
	for(var i=0; i<nb; i++) {
		forceCollapse(tab_ul[i].parentNode);
	}
}