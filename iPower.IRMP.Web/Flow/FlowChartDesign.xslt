<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:v="urn:schemas-microsoft-com:vml" version="1.0" >

  <xsl:key name="Step_Key" match="//Step" use="StepID"/>
  <xsl:key name="Param_Key" match="//Parameter" use="ParameterID"/>
  
  <xsl:template match="Process">
    <xsl:call-template name ="WorkFlowGroup"/>
	</xsl:template>
  
  <!--绘制模板-->
  <xsl:template name ="WorkFlowGroup">
    <xsl:text>&#13;</xsl:text>
    <xsl:element name ="v:group">
      <xsl:attribute name ="id">WorkFlowGroup</xsl:attribute>
      <xsl:attribute name ="coordsize">2000,2000</xsl:attribute>
      <xsl:attribute name ="style">position:absolute;width:200px;height:200px;</xsl:attribute>

      <xsl:text>&#13;</xsl:text>
      <!--StepS1-->
      <xsl:element name ="v:shapetype">
        <xsl:attribute name ="id">StepS1</xsl:attribute>
        <xsl:attribute name ="coordSize">200,200</xsl:attribute>
        <xsl:attribute name ="coordOrig">0,0</xsl:attribute>
        <xsl:attribute name ="path">m 0,100 l 50,0,150,0,200,100,150,200,50,200,0,100 x e</xsl:attribute>
        <xsl:attribute name ="strokeColor">green</xsl:attribute>
        <xsl:attribute name ="fillColor">#eeeeee</xsl:attribute>
        
        <xsl:text>&#13;</xsl:text>
        <xsl:element name ="v:path">
          <xsl:attribute name ="textboxRect">0,70,200,130</xsl:attribute>
        </xsl:element>
        <xsl:text>&#13;</xsl:text>

      </xsl:element>

      <xsl:text>&#13;</xsl:text>
      <!--StepS2-->
      <xsl:element name ="v:shapetype">
        <xsl:attribute name ="id">StepS2</xsl:attribute>
        <xsl:attribute name ="coordSize">200,200</xsl:attribute>
        <xsl:attribute name ="coordOrig">0,0</xsl:attribute>
        <xsl:attribute name ="path">m 0,100 l 50,0,150,0,200,100,150,200,50,200,0,100 x e</xsl:attribute>
        <xsl:attribute name ="strokeColor">red</xsl:attribute>
        <xsl:attribute name ="fillColor">#eeeeee</xsl:attribute>

        <xsl:text>&#13;</xsl:text>
        <xsl:element name ="v:path">
          <xsl:attribute name ="textboxRect">0,70,200,130</xsl:attribute>
        </xsl:element>
        <xsl:text>&#13;</xsl:text>

      </xsl:element>

      <xsl:text>&#13;</xsl:text>
      <!--StepM1-->
      <xsl:element name ="v:shapetype">
        <xsl:attribute name ="id">StepM1</xsl:attribute>
        <xsl:attribute name ="coordSize">200,200</xsl:attribute>
        <xsl:attribute name ="coordOrig">0,0</xsl:attribute>
        <xsl:attribute name ="path">m 0,0 l 200,0,200,200,0,200,0,0 x e</xsl:attribute>
        <xsl:attribute name ="strokeColor">green</xsl:attribute>
        <xsl:attribute name ="fillColor">#eeeeee</xsl:attribute>

        <xsl:text>&#13;</xsl:text>
        <xsl:element name ="v:path">
          <xsl:attribute name ="textboxRect">0,70,200,130</xsl:attribute>
        </xsl:element>
        <xsl:text>&#13;</xsl:text>
        
      </xsl:element>

      <xsl:text>&#13;</xsl:text>
      <!--StepM2-->
      <xsl:element name ="v:shapetype">
        <xsl:attribute name ="id">StepM2</xsl:attribute>
        <xsl:attribute name ="coordSize">200,200</xsl:attribute>
        <xsl:attribute name ="coordOrig">0,0</xsl:attribute>
        <xsl:attribute name ="path">m 0,0 l 200,0 200,200,0,200,0,0 x e</xsl:attribute>
        <xsl:attribute name ="strokeColor">red</xsl:attribute>
        <xsl:attribute name ="fillColor">#eeeeee</xsl:attribute>

        <xsl:text>&#13;</xsl:text>
        <xsl:element name ="v:path">
          <xsl:attribute name ="textboxRect">0,70,200,130</xsl:attribute>
        </xsl:element>
        <xsl:text>&#13;</xsl:text>
        
      </xsl:element>

      <xsl:text>&#13;</xsl:text>
      <!--StepE1-->
      <xsl:element name ="v:shapetype">
        <xsl:attribute name ="id">StepE1</xsl:attribute>
        <xsl:attribute name ="coordSize">200,200</xsl:attribute>
        <xsl:attribute name ="coordOrig">0,0</xsl:attribute>
        <xsl:attribute name ="path">m 50,0 at 0,0,100,200,50,0,50,200 at 100,0,200,200,150,200,150,0 x e</xsl:attribute>
        <xsl:attribute name ="strokeColor">green</xsl:attribute>
        <xsl:attribute name ="fillColor">#eeeeee</xsl:attribute>

        <xsl:text>&#13;</xsl:text>
        <xsl:element name ="v:path">
          <xsl:attribute name ="textboxRect">0,70,200,130</xsl:attribute>
        </xsl:element>
        <xsl:text>&#13;</xsl:text>

      </xsl:element>

      <xsl:text>&#13;</xsl:text>
      <!--StepE2-->
      <xsl:element name ="v:shapetype">
        <xsl:attribute name ="id">StepE2</xsl:attribute>
        <xsl:attribute name ="coordSize">200,200</xsl:attribute>
        <xsl:attribute name ="coordOrig">0,0</xsl:attribute>
        <xsl:attribute name ="path">m 50,0 at 0,0,100,200,50,0,50,200 at 100,0,200,200,150,200,150,0 x e</xsl:attribute>
        <xsl:attribute name ="strokeColor">red</xsl:attribute>
        <xsl:attribute name ="fillColor">#eeeeee</xsl:attribute>

        <xsl:text>&#13;</xsl:text>
        <xsl:element name ="v:path">
          <xsl:attribute name ="textboxRect">0,70,200,130</xsl:attribute>
        </xsl:element>
        <xsl:text>&#13;</xsl:text>

      </xsl:element>

      <xsl:text>&#13;</xsl:text>
      
      <xsl:call-template name="StepChart"/>
      <xsl:call-template name="TransChart"/>

      <xsl:text>&#13;</xsl:text>
    </xsl:element>
  </xsl:template>
  
  <!--绘制步骤-->
	<xsl:template name="StepChart">

    <xsl:variable name="ProcessID" select="ProcessID"/>
    
    <xsl:for-each select =".//Step">
      <xsl:text>&#13;</xsl:text>
      <xsl:element name="v:group">
        <xsl:attribute name="id">
          <xsl:text>g_</xsl:text>
          <xsl:value-of select ="$ProcessID"/>
          <xsl:text>_</xsl:text>
          <xsl:value-of select="StepID" />
        </xsl:attribute>
        <xsl:attribute name="style">position:relative;width:1000;height:500;left:0;top:0;Z-Index:9000</xsl:attribute>
        <xsl:attribute name="coordsize">1000,500</xsl:attribute>
        <!--<xsl:attribute name="onmouseover">OnMouseOver();</xsl:attribute>
        <xsl:attribute name="ondblclick">
          <xsl:text>javascript:ShowModalWindow("frmFlowStepEdit.aspx?ProcessID=</xsl:text>
          <xsl:value-of select ="$ProcessID"/>
          <xsl:text>&amp;StepID=</xsl:text>
          <xsl:value-of select="StepID" />
          <xsl:text>",620,430);</xsl:text>
        </xsl:attribute>-->
        <xsl:text>&#13;</xsl:text>
        <xsl:element name="v:shape">
          <xsl:attribute name="id">
            <xsl:value-of select="StepID" />
          </xsl:attribute>
          <xsl:attribute name="type">
            <xsl:choose>
              <xsl:when test="StepType = 'First'">#StepS1</xsl:when>
              <xsl:when test="StepType = 'Last'">#StepE1</xsl:when>
              <xsl:otherwise>#StepM1</xsl:otherwise>
            </xsl:choose>
          </xsl:attribute>
          <xsl:attribute name="style">
            <xsl:text>cursor:hand;position:relative;top:</xsl:text>
            <xsl:number value="floor((position()-1) div 4)*750+200" format="1"/>
            <xsl:text>;left:</xsl:text>
            <xsl:number value="((position()-1) mod 4)*1500+200" format="1"/>
            <xsl:text>;width:1000px;height:500px;</xsl:text>
          </xsl:attribute>
          <xsl:attribute name="Title">
            <xsl:text>步骤名称：</xsl:text>
            <xsl:value-of select="StepName" />
            <xsl:if test="count(.//Parameter) &gt; 0">
              <xsl:text>&#13;参数列表：&#13;</xsl:text>
              <xsl:for-each select="Parameters/Parameter">
                <xsl:text>参数名称：</xsl:text>
                <xsl:value-of select ="ParameterName"/>
                <xsl:text>,变量名称：</xsl:text>
                <xsl:value-of select ="ParameterVariable"/>
                <xsl:text>,数据类型：</xsl:text>
                <xsl:value-of select ="ParameterType"/>
                <xsl:text>&#13;</xsl:text>
              </xsl:for-each>
            </xsl:if>
          </xsl:attribute>
          <xsl:text>&#13;</xsl:text>
          <xsl:element name="v:textbox">
            <xsl:attribute name="style">text-align:center;v-text-anchor:bottom;</xsl:attribute>
            <xsl:value-of select="StepName" />
          </xsl:element>
          <xsl:text>&#13;</xsl:text>
        </xsl:element>
        <xsl:text>&#13;</xsl:text>
      </xsl:element>
      
    </xsl:for-each>
	</xsl:template>	
  
  <!--绘制关系-->
  <xsl:template name ="TransChart">
    
    <xsl:if test="count(.//Transition) &gt; 0">
      <xsl:text>&#13;</xsl:text>
      <xsl:element name ="script">
        <xsl:attribute name ="language">javascript</xsl:attribute>
        <xsl:attribute name ="type">text/javascript</xsl:attribute>
        <xsl:text>&#13;</xsl:text>
        <xsl:for-each select =".//Transition">
          <xsl:text>DrawRelation("</xsl:text>
          <xsl:value-of select ="TransitionID"/>
          <xsl:text>","</xsl:text>
          <xsl:value-of select ="FromStepID"/>
          <xsl:text>","</xsl:text>
          <xsl:value-of select ="ToStepID"/>
          <xsl:text>","变迁规则：</xsl:text>
          <xsl:variable name ="fid" select ="FromStepID"/>
          <xsl:value-of select="key('Step_Key',$fid)/StepName"/>
          <xsl:text>=&gt;</xsl:text>
          <xsl:variable name ="tid" select ="ToStepID"/>
          <xsl:value-of select="key('Step_Key',$tid)/StepName"/>
          <xsl:text>\r\n</xsl:text>
          <xsl:if test="count(.//Condition) &gt; 0">
            <xsl:text>变迁条件：\r\n</xsl:text>
            <xsl:for-each select =".//Condition">
              <xsl:variable name ="pid" select="ParameterID"/>
              <xsl:text>参数名称：</xsl:text>
              <xsl:value-of select ="key('Param_Key',$pid)/ParameterName"/>
              <xsl:text>， 结果值：</xsl:text>
              <xsl:value-of select ="CompareValue"/>
              <xsl:text>\r\n</xsl:text>
            </xsl:for-each>
          </xsl:if>
          <xsl:text>")&#13;</xsl:text>
        </xsl:for-each>
      </xsl:element>
    </xsl:if>
  </xsl:template>
</xsl:stylesheet>