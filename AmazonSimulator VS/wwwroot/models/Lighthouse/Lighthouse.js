class Lighthouse extends THREE.Group {
    constructor() {
        super();

        this.init();
    }

    init() {

        var selfRef = this;

        var mtlLoader = new THREE.MTLLoader();
        mtlLoader.setPath('models/Lighthouse/');
        var url = "lighthouse.mtl";
        mtlLoader.load(url, function (materials) {

            materials.preload();

            var objLoader = new THREE.OBJLoader();
            objLoader.setMaterials(materials);
            objLoader.setPath('models/Lighthouse/');
            objLoader.load('lighthouse.obj', function (object) {
                var group = new THREE.Group();
                object.scale.set(3, 3, 3);
                object.rotation.y = Math.PI / 2;
                object.position.set(27.5, 4.2, -6.75);
                object.rotation.y = -Math.PI / 1.125;

                group.add(object);

                var light = new THREE.SpotLight(0xffffff, 1, 10);
                light.position.set(27.85, 9.5, -6.9);
                group.add(placeholder);

                var geometry = new THREE.SphereGeometry(2.5, 2.5, 2.5);
                var material = new THREE.MeshBasicMaterial({ color: 0xffb2b2 });
                var placeholder = new THREE.Mesh(geometry, material);
                placeholder.position.set(30, 9.5, -30);
                placeholder.rotation.y += Math.PI / 16;
                group.add(light);

                selfRef.add(group);
            });

        });
    }

}
