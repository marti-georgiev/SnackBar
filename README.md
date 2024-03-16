## :pencil: Project Description
Предложеният софтуер има за цел да предостави удобен за потребителя интерфейс както за администраторите, така и за гостите, което им позволява да взаимодействат ефективно с продуктовия инвентар. Системата ще позволи на администраторите да управляват продуктовия каталог чрез добавяне на нови артикули, актуализиране на техните данни и следене на наличните количества. Освен това администраторите имат привилегията да предоставят достъп на други администратори, като гарантират правилно управление на софтуера.

От друга страна, гостите ще имат опростен изглед на софтуера, който им позволява да преглеждат списъка с продукти, да преглеждат подробности като цена и количество и да резервират артикули за ограничено време от 30 минути. Тази функция позволява на гостите да осигурят желаните от тях продукти, преди да направят покупка.

Софтуерът ще поддържа множество устройства, включително компютри и таблети, за да се погрижи за различните потребителски предпочитания и да осигури гъвкавост при достъпа до приложението.

Основните цели на този проект са:

Разработете удобно софтуерно приложение за визуализация и резервация на продукти.
Внедрете сигурна система за влизане с различни потребителски роли (администратор и гост).
Осигурете на администраторите възможността да управляват продуктовия каталог, включително добавяне, актуализиране и модифициране на продукти и потребителски достъп.
Дайте възможност на гостите да преглеждат информация за продукта, да проверяват наличността и да резервират продукти за ограничен период от време.
Осигурете съвместимост и отзивчивост на софтуера на различни устройства, като компютри и таблети.
Създавайки този софтуер, ние се стремим да подобрим потребителското изживяване и да рационализираме процеса на управление на продукта, като в крайна сметка подобрим ефективността и удовлетвореността на клиентите.

## :floppy_disk: Database Diagram


[Uploa<mxfile host="app.diagrams.net" modified="2024-03-16T11:10:39.342Z" agent="Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:123.0) Gecko/20100101 Firefox/123.0" etag="NUKiy3lbRDWncdedqZ6V" version="23.1.5" type="device">
  <diagram name="Page-1" id="PZtFRfwqLdtAOUHOoX7o">
    <mxGraphModel dx="1793" dy="943" grid="1" gridSize="10" guides="1" tooltips="1" connect="1" arrows="1" fold="1" page="1" pageScale="1" pageWidth="1100" pageHeight="850" math="0" shadow="0">
      <root>
        <mxCell id="0" />
        <mxCell id="1" parent="0" />
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-7" value="administrators" style="swimlane;fontStyle=0;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;" parent="1" vertex="1">
          <mxGeometry x="20" y="530" width="150" height="60" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-9" value="login_key" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-7" vertex="1">
          <mxGeometry y="30" width="150" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-11" value="products" style="swimlane;fontStyle=0;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;" parent="1" vertex="1">
          <mxGeometry x="160" y="280" width="320" height="240" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-12" value="id (unique)" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-11" vertex="1">
          <mxGeometry y="30" width="320" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-13" value="type" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-11" vertex="1">
          <mxGeometry y="60" width="320" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-24" value="total_available" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-11" vertex="1">
          <mxGeometry y="90" width="320" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-5" value="price" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-11" vertex="1">
          <mxGeometry y="120" width="320" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-25" value="total_reserved" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-11" vertex="1">
          <mxGeometry y="150" width="320" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-47" value="product_image (as binary)" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-11" vertex="1">
          <mxGeometry y="180" width="320" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-6" value="last_order" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-11" vertex="1">
          <mxGeometry y="210" width="320" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-15" value="shoping_carts" style="swimlane;fontStyle=0;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;" parent="1" vertex="1">
          <mxGeometry x="210" y="30" width="330" height="120" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-16" value="id (unique)" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-15" vertex="1">
          <mxGeometry y="30" width="330" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-17" value="map of products {snickers: 2, chips : 1}" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-15" vertex="1">
          <mxGeometry y="60" width="330" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-18" value="generated_when" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-15" vertex="1">
          <mxGeometry y="90" width="330" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-21" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;entryX=0.993;entryY=0.1;entryDx=0;entryDy=0;entryPerimeter=0;" parent="1" source="JRdX1ZKNMMB0ET0K1wpd-17" target="JRdX1ZKNMMB0ET0K1wpd-11" edge="1">
          <mxGeometry relative="1" as="geometry">
            <Array as="points">
              <mxPoint x="30" y="145" />
              <mxPoint x="30" y="300" />
              <mxPoint x="373" y="300" />
            </Array>
          </mxGeometry>
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-39" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="JRdX1ZKNMMB0ET0K1wpd-30" target="JRdX1ZKNMMB0ET0K1wpd-35" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-30" value="Guest (shopping cart id)" style="swimlane;fontStyle=0;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;" parent="1" vertex="1">
          <mxGeometry x="580" y="300" width="140" height="60" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-35" value="guest_functions" style="swimlane;fontStyle=0;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;" parent="1" vertex="1">
          <mxGeometry x="580" y="440" width="140" height="150" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-37" value="add product" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-35" vertex="1">
          <mxGeometry y="30" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-38" value="delete product" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-35" vertex="1">
          <mxGeometry y="60" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-43" value="cancel cart" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-35" vertex="1">
          <mxGeometry y="90" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-45" value="mark as bought" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-35" vertex="1">
          <mxGeometry y="120" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-40" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;entryX=1;entryY=0.75;entryDx=0;entryDy=0;" parent="1" source="JRdX1ZKNMMB0ET0K1wpd-16" target="JRdX1ZKNMMB0ET0K1wpd-30" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-48" value="admin_functions" style="swimlane;fontStyle=0;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;" parent="1" vertex="1">
          <mxGeometry x="30" y="620" width="140" height="150" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-49" value="edit product&amp;nbsp;" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-48" vertex="1">
          <mxGeometry y="30" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-50" value="add new product" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-48" vertex="1">
          <mxGeometry y="60" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-71" value="new admin key" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-48" vertex="1">
          <mxGeometry y="90" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-51" value="scan pdf" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-48" vertex="1">
          <mxGeometry y="120" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-52" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;entryX=0.5;entryY=0;entryDx=0;entryDy=0;" parent="1" source="JRdX1ZKNMMB0ET0K1wpd-9" target="JRdX1ZKNMMB0ET0K1wpd-48" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-53" value="edit_product" style="swimlane;fontStyle=0;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;" parent="1" vertex="1">
          <mxGeometry x="285" y="680" width="140" height="180" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-54" value="add count" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-53" vertex="1">
          <mxGeometry y="30" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-55" value="remove count" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-53" vertex="1">
          <mxGeometry y="60" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-59" value="change image" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-53" vertex="1">
          <mxGeometry y="90" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-60" value="delete product" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-53" vertex="1">
          <mxGeometry y="120" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-56" value="change price" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-53" vertex="1">
          <mxGeometry y="150" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-69" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;entryX=0.997;entryY=0.05;entryDx=0;entryDy=0;entryPerimeter=0;" parent="1" source="JRdX1ZKNMMB0ET0K1wpd-62" target="JRdX1ZKNMMB0ET0K1wpd-11" edge="1">
          <mxGeometry relative="1" as="geometry">
            <Array as="points">
              <mxPoint x="450" y="675" />
              <mxPoint x="500" y="675" />
              <mxPoint x="500" y="292" />
            </Array>
          </mxGeometry>
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-62" value="add_new_product" style="swimlane;fontStyle=0;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;" parent="1" vertex="1">
          <mxGeometry x="285" y="550" width="140" height="60" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-66" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;entryX=-0.007;entryY=0.142;entryDx=0;entryDy=0;entryPerimeter=0;" parent="1" source="JRdX1ZKNMMB0ET0K1wpd-50" target="JRdX1ZKNMMB0ET0K1wpd-62" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-70" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;entryX=-0.021;entryY=0.078;entryDx=0;entryDy=0;entryPerimeter=0;" parent="1" source="JRdX1ZKNMMB0ET0K1wpd-49" target="JRdX1ZKNMMB0ET0K1wpd-53" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-73" value="scan pdf" style="swimlane;fontStyle=0;childLayout=stackLayout;horizontal=1;startSize=30;horizontalStack=0;resizeParent=1;resizeParentMax=0;resizeLast=0;collapsible=1;marginBottom=0;whiteSpace=wrap;html=1;" parent="1" vertex="1">
          <mxGeometry x="30" y="810" width="140" height="120" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-74" value="Item 1" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-73" vertex="1">
          <mxGeometry y="30" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-75" value="Item 2" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-73" vertex="1">
          <mxGeometry y="60" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-76" value="Item 3" style="text;strokeColor=none;fillColor=none;align=left;verticalAlign=middle;spacingLeft=4;spacingRight=4;overflow=hidden;points=[[0,0.5],[1,0.5]];portConstraint=eastwest;rotatable=0;whiteSpace=wrap;html=1;" parent="JRdX1ZKNMMB0ET0K1wpd-73" vertex="1">
          <mxGeometry y="90" width="140" height="30" as="geometry" />
        </mxCell>
        <mxCell id="JRdX1ZKNMMB0ET0K1wpd-77" style="edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;" parent="1" source="JRdX1ZKNMMB0ET0K1wpd-51" target="JRdX1ZKNMMB0ET0K1wpd-73" edge="1">
          <mxGeometry relative="1" as="geometry" />
        </mxCell>
      </root>
    </mxGraphModel>
  </diagram>
</mxfile>
ding project.drawio…]()


## :hammer: Used technologies

<details>
  <summary>Client</summary>
  <ul>
    <li>HTML & CSS</li>
    <li>JavaScript</li>
    
  </ul>
</details>

<details>
  <summary>Тechnologies</summary>
  <ul>
    <li>ASP.Net Core</li>
    <li>Entity Framework Core</li>
    <li>ASP.Net MVC</li>
  </ul>
</details>

<details>
<summary>Database</summary>
  <ul>
    <li><a href="https://www.sqlite.org/">SQL Lite</a></li>
     
  </ul>
</details>



### :gear: Installation

